using Application.Abstractions.Services;
using Application.UseCases.Queries.Accounts.Login;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Account.Logout;
using MediatR;

namespace Application.UserCases.Commands.Users;

public class LogoutAccountCommandHandler(IRedisService _redisService)
    : ICommandHandler<LogoutCommand>
{
    async Task<Result.Success> IRequestHandler<LogoutCommand, Result.Success>.Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (! request.logoutUserId.Equals(request.currentUserId))
        {
            // throw new error
            throw new Exception("UserId Conflict");
        }
        await _redisService.RemoveAsync(LoginAccountCommandHandler.Redis_Prefix + request.logoutUserId);
        return Result.Success.Logout();
    }
}
