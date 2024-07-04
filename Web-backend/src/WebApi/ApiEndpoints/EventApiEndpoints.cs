using Carter;
using Contract.Services.Event.Create;
using Contract.Services.Event.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class EventApiEndpoints : CarterModule
{
    public EventApiEndpoints() : base("api/events")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //api/events
        app.MapPost(string.Empty, async (ISender sender, [FromBody] CreateEventCommand CreateEventCommand) =>
        {
            var result = await sender.Send(CreateEventCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });

        //api/e/1
        app.MapGet("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });
    }
}
