using Contract.Abstractions.Dtos.Search;
using Contract.Abstractions.Messages;
using Contract.Services.Branch.Share;

namespace Contract.Services.Branch.GetBranches;
public record GetBranchesQuery(Guid BusinessId) : IQuery<SearchResponse<List<BranchResponse>>>;
