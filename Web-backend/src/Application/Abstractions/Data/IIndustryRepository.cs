using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IIndustryRepository
{
    void AddRange(List<Industry> industries);
    Task<bool> IsAllIndustryIdsExistAsync(List<Guid> industryIds);
}
