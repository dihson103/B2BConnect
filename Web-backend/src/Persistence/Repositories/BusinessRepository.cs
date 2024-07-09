using Application.Abstractions.Data;
using Contract.Services.Business.GetBusinesses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class BusinessRepository : IBusinessRepository
{
    private readonly AppDbContext _context;
    public BusinessRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Business> GetByIdAsync(Guid id)
    {
        return await _context.Businesses
                     .AsNoTracking()
                     .AsSplitQuery()
                     .Include(e => e.Representative)
                    .Include(e => e.Branches!)
                    .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> IsBusinessValidAsync(Guid id)
    {
        return await _context.Businesses
            .Include(b => b.Account)
            .AnyAsync(b => b.Id == id && b.Account.IsActive == true);
    }

    public async Task<(List<Business>?, int)> SearchBusinessAsync(GetBusinessesQuery getBusinessesQuery)
    {
        var query = _context.Businesses!.Where(b => b!.IsVerified == getBusinessesQuery.IsVerified);

        var searchTerm = getBusinessesQuery.SearchTerm;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(b => b.Name.Contains(searchTerm));
        }

        var totalItems = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalItems / getBusinessesQuery.PageSize);

        var businesses = await query
            .OrderBy(b => b.DateOfEstablishment)
            .Skip((getBusinessesQuery.PageIndex - 1) * getBusinessesQuery.PageSize)
            .Take(getBusinessesQuery.PageSize)
            .Include(b => b.Representative)
            .Include(b => b.Branches)
            .AsNoTracking()
            .AsSingleQuery()
            .ToListAsync();

        return (businesses, totalPages);
    }
}
