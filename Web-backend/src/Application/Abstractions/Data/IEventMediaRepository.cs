using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IEventMediaRepository
{
    void AddRange(List<EventMedia> eventMedias);
}
