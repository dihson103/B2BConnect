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
        Console.WriteLine("header " + header);
        if (header is null || !header.StartsWith("Bearer "))
        {
            await _next(context);
            return;
        }

        var account = context.User;
        if (account?.Identity?.IsAuthenticated == true)
        foreach (var claim in account.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
        {
            var accountId = account?.FindFirst("Id")?.Value;
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