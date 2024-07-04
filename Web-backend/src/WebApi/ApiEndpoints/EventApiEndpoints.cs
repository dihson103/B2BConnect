using Carter;
using Contract.Services.Event.Create;
using Contract.Services.Event.GetById;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Update;
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
        app.MapPost(string.Empty, async (ISender sender, [FromBody] CreateEventCommand CreateEventCommand) =>
        {
            var result = await sender.Send(CreateEventCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });

        app.MapGet("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });

        app.MapGet(string.Empty, async (ISender sender, [AsParameters] GetEventsQuery query) =>
        {
            var result = await sender.Send(query);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });

        app.MapPut("{id}", async (ISender sender, [FromRoute] Guid id, [FromBody] UpdateEventRequest request) =>
        {
            var updateEventCommand = new UpdateEventCommand(id, request);
            var result = await sender.Send(updateEventCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Events api" } }
        });
    }
}
