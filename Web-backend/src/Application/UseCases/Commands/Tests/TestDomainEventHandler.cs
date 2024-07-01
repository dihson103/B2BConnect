using Contract.Abstractions.Messages;
using Contract.Services.Tests;

namespace Application.UseCases.Commands.Tests;
internal sealed class TestDomainEventHandler : IDomainEventHandler<TestDomainEvent>
{
    public async Task Handle(TestDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Tesssssst " + notification.Message);

        // Wait for one minute
        await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

        // Code to execute after the delay
        Console.WriteLine("One minute has passed.");
    }
}
