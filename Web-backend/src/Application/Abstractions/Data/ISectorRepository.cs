using Domain.Entities;

namespace Application.Abstractions.Data;
public interface ISectorRepository
{
    Task<List<Sector>> GetSectorsByBusinessIdAsync(Guid businessId);
}
