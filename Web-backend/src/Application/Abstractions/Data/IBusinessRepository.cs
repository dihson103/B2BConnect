using Contract.Services.Business.GetBusinesses;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IBusinessRepository
{
    Task<(List<Business>?, int, int)> SearchBusinessAsync(GetBusinessesQuery getBusinessesQuery);

    Task<Business> GetByIdAsync(Guid id);
    Task<bool> IsBusinessValidAsync(Guid id);

}
