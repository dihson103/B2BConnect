using Contract.Abstractions.Messages;
using Contract.Services.Event.GetEvents;

namespace Contract.Services.Event.GetById;
public record GetByIdQuery(Guid Id) : IQuery<EventResponse>;
