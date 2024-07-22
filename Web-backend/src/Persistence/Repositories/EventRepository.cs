using Application.Abstractions.Data;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Share;
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

    public async Task<Event> GetByIdIncludeIndustriesAsync(Guid id)
    {
        return await _context.Events
            .AsNoTracking()
            .Include(e => e.EventMedias)
                .ThenInclude(e => e.Media)
            .Include(e => e.EventIndustries)
                .ThenInclude(e => e.Industry)
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> IsEventExistAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsEventValidToJoinAsync(Guid id)
    {
        return await _context.Events.AnyAsync(e => e.Id == id && e.Status == EventStatus.PLANNING);
    }

    public async Task<(List<Event>, int, int)> SearchEventsAsync(GetEventsQuery request)
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
            .Include(e => e.EventMedias)
                .ThenInclude(em => em.Media)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        return (events, totalPages, totalItems);
    }

    public void Update(Event Event)
    {
        _context.Events.Update(Event);
    }
}
