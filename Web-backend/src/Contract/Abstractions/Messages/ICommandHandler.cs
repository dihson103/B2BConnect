using Contract.Abstractions.Dtos.Results;
using MediatR;

namespace Contract.Abstractions.Messages;
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result.Success<TResponse>>
    where TCommand : ICommand<TResponse>
{
}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result.Success>
    where TCommand : ICommand
{ 
}
