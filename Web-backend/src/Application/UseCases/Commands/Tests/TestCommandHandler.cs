using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Tests;

namespace Application.UseCases.Commands.Tests;
internal class TestCommandHandler : ICommandHandler<TestCommand>
{
    public Task<Result.Success> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
