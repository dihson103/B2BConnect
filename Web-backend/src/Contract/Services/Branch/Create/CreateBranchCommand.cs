using Contract.Abstractions.Messages;

namespace Contract.Services.Branch.Create;
public record CreateBranchCommand(
    string? Email,
    string? Phone,
    string Address,
    bool IsMainBranch,
    Guid BusinessId) : ICommand;

