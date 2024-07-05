using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

                int statusCode = StatusCodes.Status500InternalServerError;
                string title = "Server error";

                switch (ex)
                {
                    case BadHttpRequestException:
                        statusCode = StatusCodes.Status400BadRequest;
                        title = "Bad request";
                        break;
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        title = "Not found, there is no object with that Id";
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

                context.Response.StatusCode = statusCode;

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

                await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: CancellationToken.None);
            }
        }
    }
}