using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class ParticipationRepository : IParticipationRepository
{
    private readonly AppDbContext _context;

    public ParticipationRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Participation participation)
    {
        _context.Participations.Add(participation);
    }

    public async Task<bool> IsBusinessRequestedAsync(Guid businessId, Guid eventId)
    {
        return await _context.Participations.AnyAsync(p => p.BusinessId == businessId && p.EventId == eventId);
    }
}
