using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
namespace Schematix.Api.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Server Error",
                Detail = "Something Went wrong"
            };

            var jsonproblem = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(jsonproblem);

            context.Response.ContentType = "application/json";
        }
    }
}
