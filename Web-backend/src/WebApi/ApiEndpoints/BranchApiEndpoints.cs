using Carter;
using Contract.Services.Branch.Create;
using Contract.Services.Branch.Delete;
using Contract.Services.Branch.GetBranch;
using Contract.Services.Branch.GetBranches;
using Contract.Services.Branch.Update;
using Contract.Services.Event.Create;
using Contract.Services.Event.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class BranchApiEndpoints : CarterModule
{
    public BranchApiEndpoints() : base("api/branches")
    {
        
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, async (ISender sender, [AsParameters] GetBranchesQuery getBranchesQuery) =>
        {
            var result = await sender.Send(getBranchesQuery);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Branches api" } }
        });

        app.MapGet("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Branches api" } }
        });

        app.MapPost(string.Empty, async (ISender sender, [FromBody] CreateBranchCommand createBranchCommand) =>
        {
            var result = await sender.Send(createBranchCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Branches api" } }
        });

        app.MapPut("{id}", async (ISender sender, [FromRoute] Guid id, [FromBody] UpdateBranchRequest request) =>
        {
            var updateBranchCommand = new UpdateBranchCommand(id, request);
            var result = await sender.Send(updateBranchCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Branches api" } }
        });

        app.MapDelete("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var deleteBranchCommand = new DeleteBranchCommand(id);
            var result = await sender.Send(deleteBranchCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Branches api" } }
        });
    }
}
