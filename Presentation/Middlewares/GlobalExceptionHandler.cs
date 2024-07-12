using Domain.Exceptions;
using Presentation.Responses;

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
                logger.LogError(ex, $"Exception occurred: {ex.Message}");

                var statusCode = StatusCodes.Status500InternalServerError;
                var message = "Server error";

                switch (ex)
                {
                    case ArgumentException:
                    case FluentValidation.ValidationException:
                        statusCode = StatusCodes.Status400BadRequest;
                        message = ex.Message;
                        break;
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        message = ex.Message;
                        break;
                    case UnauthorizedAccessException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        message = "Unauthorized";
                        break;
                    case NotImplementedException:
                        statusCode = StatusCodes.Status501NotImplemented;
                        message = "Not implemented";
                        break;
                }

                context.Response.StatusCode = statusCode;

                var exceptionResponse = new ExceptionResponse
                {
                    Message = message
                };

                await context.Response.WriteAsJsonAsync(exceptionResponse);
            }
        }
    }
}