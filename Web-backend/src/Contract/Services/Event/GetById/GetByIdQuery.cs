using Contract.Abstractions.Messages;

namespace Contract.Services.Event.GetById;
public record GetByIdQuery(Guid Id) : IQuery<SingleEventResponse>;
