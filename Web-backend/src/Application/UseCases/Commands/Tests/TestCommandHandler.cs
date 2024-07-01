using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Tests;
using MediatR;

namespace Application.UseCases.Commands.Tests;
internal class TestCommandHandler(IPublisher _publisher) : ICommandHandler<TestCommand>
{
    public async Task<Result.Success> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        var testDomainEvent = new TestDomainEvent(Guid.NewGuid(), "Hello ace");
        await _publisher.Publish(testDomainEvent, cancellationToken);

        return Result.Success.Create();
    }
}
