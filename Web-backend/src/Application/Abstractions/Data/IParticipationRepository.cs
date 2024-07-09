using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IParticipationRepository
{
    void Add(Participation participation);
    Task<bool> IsBusinessRequestedAsync(Guid businessId, Guid eventId);
}
