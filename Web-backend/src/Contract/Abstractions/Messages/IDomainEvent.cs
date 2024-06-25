using MediatR;

namespace Contract.Abstractions.Messages;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
