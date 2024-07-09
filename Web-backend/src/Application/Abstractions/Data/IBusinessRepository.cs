using Contract.Services.Business.GetBusinesses;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IBusinessRepository
{
    Task<(List<Business>?, int)> SearchBusinessAsync(GetBusinessesQuery getBusinessesQuery);

    Task<Business> GetByIdAsync(Guid id);

}
