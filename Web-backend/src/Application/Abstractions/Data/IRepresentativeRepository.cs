
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IRepresentativeRepository
{
    void Add(Representative representative);

    void DeleteByBusinessId(Guid businessId);
    void Update(Representative re);
}
