using Contract.Abstractions.Dtos.Results;
using MediatR;

namespace Contract.Abstractions.Messages;

public interface ICommand<TResponse> : IRequest<Result.Success<TResponse>>
{
}

public interface ICommand : IRequest<Result.Success>
{
}
