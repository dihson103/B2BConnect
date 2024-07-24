using Carter;
// using Contract.Services.Account.Login;
// using Contract.Services.User.Logout;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Contract.Services.Account.Login;
using Contract.Services.Account.Logout;
using Contract.Services.Account.Create;


namespace WebApi.ApiEndpoints;

public class AuthEndpoints : CarterModule
{
    public AuthEndpoints() : base("/api/auth")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (ISender sender, [FromBody] LoginCommand loginCommand) =>
        {
            var result = await sender.Send(loginCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Authentication api" } }
        });

        app.MapPost("/logout/{id}", async (ISender sender, ClaimsPrincipal claim, [FromRoute] string id) =>
        {
            var userId = claim.FindFirst("UserId")?.Value;
            var logoutCommand = new LogoutCommand(userId, id);
            var result = await sender.Send(logoutCommand);

            return Results.Ok(result);
        }).RequireAuthorization().WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Authentication api" } }
        });

        app.MapPost("/register", async (ISender sender, [FromBody] CreateAccountCommand createAccountCommand) =>
        {
            var result = await sender.Send(createAccountCommand);
            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Authentication api" } }
        });
    }
}
