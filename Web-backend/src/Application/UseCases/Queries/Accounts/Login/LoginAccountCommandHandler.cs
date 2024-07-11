using System.Diagnostics.Tracing;
using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Abstractions.Dtos.Results;
using Contract.Abstractions.Messages;
using Contract.Services.Account.Create;
using Contract.Services.Event.Create;
using Contract.Services.Account.Login;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;
using Contract.Services.Account.SharedDto;

namespace Application.UseCases.Queries.Accounts.Login;
public class LoginAccountCommandHandler(
    IAccountRepository _accountRepository,
    IPasswordService _passwordService,
    IRedisService _redisService,
    IJwtService _jwtService) : ICommandHandler<LoginCommand, LoginResponse>
{
    public static readonly string Redis_Prefix = "USER-";
    public async Task<Result.Success<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.LoginAsync(request.Email) ?? throw new MyUnauthorizedException("Sai mật khẩu hoặc password.");
        var isPasswordValid = _passwordService.IsVerify(account!.Password, request.Password);
        if (!isPasswordValid)
        {
            throw new MyUnauthorizedException("Sai mật khẩu hoặc password.");
        }
        var accessToken = _jwtService.GenerateAccessToken(account);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var accountResponse = new AccountResponse(account.Email, account.Id, account.IsAdmin);
        var loginResponse = new LoginResponse(accountResponse, accessToken, refreshToken);
        await _redisService.SetAsync<LoginResponse>($"USER-{account.Id}", loginResponse);
        return Result.Success<LoginResponse>.Get(loginResponse);
    }
}
