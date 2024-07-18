using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IMediaRepository
{
    void AddRange(List<Media> medias);
}
