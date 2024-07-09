using Contract.Abstractions.Messages;

namespace Contract.Services.Event.RequestJoin;
public record RequestJoinCommand(Guid BusinessId, Guid EventId) : ICommand;
