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

                var statusCode = StatusCodes.Status500InternalServerError;
                var title = "Server error";

                switch (ex)
                {
                    case ArgumentException:
                        statusCode = StatusCodes.Status400BadRequest;
                        title = ex.Message;
                        break;
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        title = ex.Message;
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
                };

                await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: CancellationToken.None);
            }
        }
    }
}