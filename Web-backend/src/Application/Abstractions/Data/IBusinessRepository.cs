using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.Share;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IBusinessRepository
{
    Task<(List<Business>?, int, int)> SearchBusinessAsync(GetBusinessesByUserQuery getBusinessesQuery);

    Task<Business> GetByIdAsync(Guid id);
    Task<bool> IsBusinessValidAsync(Guid id);
    void Add(Business business);
    void Update(Business business);
    Task<(List<BusinessWaitingVerifyResponse>?, int, int)> SearchWaitingBusinessAsync(GetWaitingVerifyBussinessesQuery request);
    Task<(List<Business>?, int, int)> SearchBusinessesByAdminAsync(GetBusinessesByAdminQuery request);
    Task<Business> getByAccountIdAsync(Guid id);
}
