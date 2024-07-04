using Carter;
using Contract.Services.File.GetFile;
using Contract.Services.File.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.ApiEndpoints;

public class FileEndPoints : CarterModule
{
    public FileEndPoints() : base("api/files")
    {
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(string.Empty, async (ISender sender, IFormFile file) =>
        {
            var uploadFileCommand = new UploadFileCommand(file);
            var result = await sender.Send(uploadFileCommand);

            return Results.Ok(result);
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Files api" } }
        }).DisableAntiforgery();

        app.MapGet("{fileName}", async (ISender sender, [FromRoute] string fileName, HttpContext context) =>
        {
            var getFileQuery = new GetFileQuery(fileName);
            var result = await sender.Send(getFileQuery);

            byte[] fileBytes = result.Data;
            context.Response.ContentType = "image/png";
            context.Response.ContentLength = fileBytes.Length;
            await context.Response.Body.WriteAsync(fileBytes);

            return Results.Empty;
        }).WithOpenApi(x => new OpenApiOperation(x)
        {
            Tags = new List<OpenApiTag> { new() { Name = "Files api" } }
        }).DisableAntiforgery();
    }
}
