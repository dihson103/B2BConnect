using System.Net;
using Domain.Abstractioins.Exceptions;

namespace WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (MyException excepion)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.OK;

            var error = new
            {
                Status = excepion.StatusCode,
                Message = excepion?.Message ?? "My exception",
                Error = excepion?.Error ?? null,
                IsSuccess = false
            };

            await context.Response.WriteAsJsonAsync(error);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var error = new
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "Internal Server Error"
            };

            Console.WriteLine(ex.Message);

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
