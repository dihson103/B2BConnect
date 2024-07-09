using Contract.Abstractions.Messages;

namespace Contract.Services.Branch.Update;
public record UpdateBranchCommand(Guid Id, UpdateBranchRequest UpdateBranchRequest) : ICommand;

