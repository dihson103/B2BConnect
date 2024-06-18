using Carter;
using Domain.Abstractioins.Exceptions;

namespace WebApi.ApiEndpoints;

public class UserApiEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("test", () =>
        {
            throw new MyException(333, "eerrr");
            return Results.Ok("hello");
        });
    }
}
