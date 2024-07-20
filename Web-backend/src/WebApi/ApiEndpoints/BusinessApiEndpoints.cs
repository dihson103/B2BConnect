using Carter;
using Contract.Services.Business.Create;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.GetById;
using Contract.Services.Business.Share;
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
        app.MapGet(string.Empty, async (ISender sender, [FromQuery] string? searchTerm,
            [FromQuery] string? industryIds, [FromQuery] NumberOfEmployee? numberOfEmployee, 
            [FromQuery] bool isVerified = false, [FromQuery] int pageIndex = 1, 
            [FromQuery] int pageSize = 10) =>
        {
            // Parse industryIds from query string
            List<Guid>? parsedIndustryIds = null;
            if (!string.IsNullOrEmpty(industryIds))
            {
                parsedIndustryIds = industryIds.Split(',')
                    .Select(id => Guid.TryParse(id.Trim(), out var guid) ? guid : (Guid?)null)
                    .Where(guid => guid.HasValue)
                    .Select(guid => guid!.Value)
                    .ToList();
            }

            var getBusinessesQuery = new GetBusinessesQuery(
                SearchTerm: searchTerm,
                IndustryIds: parsedIndustryIds,
                NumberOfEmployee: numberOfEmployee,
                IsVerified: isVerified,
                PageIndex: pageIndex,
                PageSize: pageSize
            );

            var result = await sender.Send(getBusinessesQuery);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Businesses api" } }
        });


        app.MapGet("{id}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Businesses api" } }
        });

        app.MapGet("/waiting", async (ISender sender, [AsParameters] GetWaitingVerifyBussinessesQuery query) =>
        {
            var result = await sender.Send(query);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Businesses api" } }
        });

        app.MapPost(string.Empty, async (ISender sender, [FromBody] SaveBusinessCommand createBusinessCommand) =>
        {
            var result = await sender.Send(createBusinessCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Businesses api" } }
        });
    }
}
