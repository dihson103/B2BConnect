using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Application.Abstractions.Services;
using Application.UseCases.Queries.Accounts.Login;
using Contract.Services.Account.Login;

namespace WebApi.Middlewares;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var header = context.Request.Headers["Authorization"].FirstOrDefault();
        if (header is null || !header.StartsWith("Bearer "))
        {
            await _next(context);
            return;
        }

        var account = context.User;
        if (account?.Identity?.IsAuthenticated == true)
        {
            var accountId = account?.FindFirst("UserId")?.Value;
            var redis = context.RequestServices.GetRequiredService<IRedisService>();
            var loginResponse = await redis.GetAsync<LoginResponse>(LoginAccountCommandHandler.Redis_Prefix + accountId);
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (loginResponse is null || !loginResponse.AccessToken.Equals(token)) {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
        }
        await _next(context);
    }
}