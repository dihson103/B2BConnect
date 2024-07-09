using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class SectorRepository : ISectorRepository
{
    private readonly AppDbContext _context;

    public SectorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sector>> GetSectorsByBusinessIdAsync(Guid businessId)
    {
        return await _context.Sectors
            .AsNoTracking()
            .Where(s => s.BusinessId == businessId)
            .ToListAsync();
    }
}
