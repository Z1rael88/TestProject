using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;

namespace WebApplication1.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "Server error";

            switch (exception)
            {
                case BadHttpRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Bad request";
                    break;
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Not found,there is no object with that Id";
                    break;
                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    title = "Unauthorized";
                    break;
                case NotImplementedException:
                    statusCode = StatusCodes.Status501NotImplemented;
                    title = "Not implemented";
                    break;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}