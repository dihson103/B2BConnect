using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class EventIndustryRepository : IEventIndustryRepository
{
    private readonly AppDbContext _context;

    public EventIndustryRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRange(List<EventIndustry> eventIndustries)
    {
        _context.EventIndustries.AddRange(eventIndustries);
    }

    public async Task<List<EventIndustry>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.EventIndustries
            .Include(x => x.Industry)
            .AsNoTracking()
            .Where(x => x.EventId == eventId)
            .ToListAsync();
    }
}
