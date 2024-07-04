using Application.Abstractions.Data;
using Contract.Services.Event.GetEvents;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Event Event)
    {
        _context.Events.Add(Event);
    }

    public async Task<Event> GetByIdAsync(Guid id)
    {
        return await _context.Events
            .Include(e => e.EventIndustries)
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> IsEventExistAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<(List<Event>, int)> SearchEventsAsync(GetEventsQuery request)
    {
        var query = _context.Events
            .Where(e => e.Status == request.Status);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(e => e.Name.ToLower().Contains(search)
            || e.Location.ToLower().Contains(search)
            || e.Description.ToLower().Contains(search));
        }

        var totalItems = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        var events = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return (events, totalPages);
    }

    public void Update(Event Event)
    {
        _context.Events.Update(Event);
    }
}
