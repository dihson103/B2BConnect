using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class IndustryRepository : IIndustryRepository
{
    private readonly AppDbContext _context;

    public IndustryRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRange(List<Industry> industries)
    {
        _context.Industries.AddRange(industries);
    }

    public async Task<bool> IsAllIndustryIdsExistAsync(List<Guid> industryIds)
    {
        var numberIndustry = await _context.Industries.CountAsync(i => industryIds.Contains(i.Id));

        return numberIndustry == industryIds.Count();
    }
}
