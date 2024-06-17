using Carter;

namespace WebApi.ApiEndpoints;

public class UserApiEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("test", () =>
        {
            return Results.Ok("hello");
        });
    }
}
