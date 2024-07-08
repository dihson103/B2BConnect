using Contract.Abstractions.Messages;

namespace Contract.Services.Branch.Delete;
public record DeleteBranchCommand(Guid id) : ICommand;
