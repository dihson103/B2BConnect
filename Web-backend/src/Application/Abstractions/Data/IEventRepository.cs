using Contract.Services.Event.GetEvents;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IEventRepository
{
    void Add(Event Event);
    void Update(Event Event);
    Task<bool> IsEventExistAsync(string name);
    Task<Event> GetByIdAsync(Guid id);
    Task<(List<Event>, int)> SearchEventsAsync(GetEventsQuery request);
}
