using Domain.Entities;

namespace Application.Abstractions.Data;
public interface ISectorRepository
{
    Task<List<Sector>> GetSectorsByBusinessIdAsync(Guid businessId);

    void Add(Sector sector);

    void AddRange(List<Sector> sectors);
    void DeleteByBusinessId(Guid id);
}
