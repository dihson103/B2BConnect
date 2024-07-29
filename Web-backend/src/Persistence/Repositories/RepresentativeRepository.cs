using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class RepresentativeRepository : IRepresentativeRepository
{
    private readonly AppDbContext _context;
    public RepresentativeRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add(Representative representative)
    {
        _context.Representatives.Add(representative);
    }

    public void Attach(Representative re)
    {
        _context.Set<Representative>().Attach(re);
    }

    public void DeleteByBusinessId(Guid businessId)
    {
        var representatives = _context.Representatives.Where(r => r.BusinessId == businessId);

        if (representatives.Any())
        {
            _context.Representatives.RemoveRange(representatives);
        }
    }

    public async Task<Representative> GetByBusinessId(Guid businessId)
    {
        return await _context.Representatives
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.BusinessId == businessId);
    }

    public void Update(Representative re)
    {
        var existingEntity = _context.Representatives.Local
            .FirstOrDefault(r => r.Id == re.Id);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(re);
        }
        else
        {
            _context.Representatives.Update(re);
        }
    }
}
