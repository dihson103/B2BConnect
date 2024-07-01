using Carter;
using Contract.Services.Tests;
using Domain.Abstractioins.Exceptions;
using MediatR;

namespace WebApi.ApiEndpoints;

public class UserApiEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("test", async (ISender sender) =>
        {
            var testCommand = new TestCommand();
            var result = await sender.Send(testCommand); 

            return Results.Ok(result);
        });
    }
}
