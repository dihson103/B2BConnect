
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IRepresentativeRepository
{
    void Add(Representative representative);
    void Attach(Representative re);
    void DeleteByBusinessId(Guid businessId);
    Task<Representative> GetByBusinessId(Guid businessId);
    void Update(Representative re);
}
