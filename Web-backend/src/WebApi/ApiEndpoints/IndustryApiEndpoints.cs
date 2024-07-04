using Carter;
using Contract.Services.Industry.SearchIndustries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class IndustryApiEndpoints : CarterModule
{
    public IndustryApiEndpoints() : base("api/industries")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, async (ISender sender, [FromQuery] string? searchTerm) =>
        {
            var searchIndustriesQuery = new SearchIndustriesQuery(searchTerm);
            var result = await sender.Send(searchIndustriesQuery);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Industries api" } }
        });
    }
}
