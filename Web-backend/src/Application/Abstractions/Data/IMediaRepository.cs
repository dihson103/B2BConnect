using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IMediaRepository
{
    void AddRange(List<Media> medias);
    Task<bool> IsAllMediasExistAsync(List<Guid> Ids);
}
