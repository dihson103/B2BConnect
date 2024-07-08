using Carter;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class BusinessApiEndpoints : CarterModule
{
    public BusinessApiEndpoints() : base("api/businesses")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(string.Empty, async (ISender sender, [AsParameters] GetBusinessesQuery getProductsQuery) =>
        {
            var result = await sender.Send(getProductsQuery);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Business api" } }
        });

        app.MapGet("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Business api" } }
        });
    }
}
