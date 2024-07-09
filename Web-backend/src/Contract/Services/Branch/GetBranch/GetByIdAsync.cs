using Contract.Abstractions.Messages;
using Contract.Services.Branch.Share;

namespace Contract.Services.Branch.GetBranch;
public record GetByIdQuery(Guid Id) : IQuery<BranchResponse>;
