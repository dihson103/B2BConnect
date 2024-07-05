using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IEventIndustryRepository
{
    void AddRange(List<EventIndustry> eventIndustries);
    Task<List<EventIndustry>> GetByEventIdAsync(Guid eventId);
}
