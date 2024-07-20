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

    public void Add(Sector sector)
    {
       _context.Sectors.Add(sector);
    }

    public void AddRange(List<Sector> sectors)
    {
        _context.Sectors.AddRange(sectors);
    }

    public void DeleteByBusinessId(Guid id)
    {
        var sectors = _context.Sectors.Where(r => r.BusinessId == id);

        if (sectors.Any())
        {
            _context.Sectors.RemoveRange(sectors);
        }
    }

    public async Task<List<Sector>> GetSectorsByBusinessIdAsync(Guid businessId)
    {
        return await _context.Sectors
            .AsNoTracking()
            .Where(s => s.BusinessId == businessId)
            .ToListAsync();
    }
}
