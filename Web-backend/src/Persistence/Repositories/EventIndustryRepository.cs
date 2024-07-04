using Application.Abstractions.Data;
using Domain.Entities;

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
}
