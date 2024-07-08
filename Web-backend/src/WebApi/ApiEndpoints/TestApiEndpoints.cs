using Carter;
using Contract.Services.Tests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class TestApiEndpoints : CarterModule
{
    public TestApiEndpoints() : base("api/test")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("email/{email}", async (ISender sender, [FromRoute] string email) =>
        {
            var testSendMail = new TestSendMail(email);
            var result = await sender.Send(testSendMail);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Tests api" } }
        });
    }
}
