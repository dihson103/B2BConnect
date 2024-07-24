using Contract.Services.Branch.GetBranches;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IBranchRepository
{
    void Add(Branch branch);
    void Update(Branch branch);

    void Delete(Branch branch);
    Task<List<Branch>> GetBranchesAsync(GetBranchesQuery getBranchesQuery);
    Task<Branch> GetByIdAsync(Guid id);
    void AddRange(List<Branch> branches);
    void DeleteByBusinessId(Guid id);
    void DeleteMainBranchOfBusiness(Guid businessId);
}
