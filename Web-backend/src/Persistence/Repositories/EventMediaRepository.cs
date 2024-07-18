using Application.Abstractions.Data;
using Domain.Entities;

namespace Persistence.Repositories;
internal class EventMediaRepository : IEventMediaRepository
{
    private readonly AppDbContext _context;

    public EventMediaRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRange(List<EventMedia> eventMedias)
    {
        _context.EventMedias.AddRange(eventMedias);
    }
}
