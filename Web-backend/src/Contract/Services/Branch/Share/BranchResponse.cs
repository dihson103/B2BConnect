
namespace Contract.Services.Branch.Share;
public record BranchResponse(Guid Id, string? Email, string? Phone, string Address,
    bool IsMainBranch, Guid BusinessId);
