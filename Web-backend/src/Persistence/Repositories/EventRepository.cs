using Application.Abstractions.Data;
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
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> IsEventExistAsync(string name)
    {
        throw new NotImplementedException();
    }
}
