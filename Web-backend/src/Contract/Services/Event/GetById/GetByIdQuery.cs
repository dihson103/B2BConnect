using Contract.Abstractions.Messages;

namespace Contract.Services.Event.GetById;
public record GetByIdQuery(int Id) : IQuery<EventResponse>;
