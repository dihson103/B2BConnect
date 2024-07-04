using Contract.Abstractions.Messages;

namespace Contract.Services.Event.Update;
public record UpdateEventCommand(Guid Id, UpdateEventRequest UpdateEventRequest) : ICommand;
