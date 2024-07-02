using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
       _logger.LogError(exception,"Exception occured: {Message}", exception.Message);

       var problemDetails = new ProblemDetails()
       {
           Status = StatusCodes.Status500InternalServerError,
           Title = "Server error",
           Type = "Server error"
       };
       httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

       await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
       return true;
    }
}