using Microsoft.AspNetCore.Diagnostics;
using Accounting.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.App.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
                BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception,
                    "Unhandled Exception Occurred! Path: {Path} | Method: {Method} | TraceId: {TraceId}",
                    httpContext.Request.Path,
                    httpContext.Request.Method,
                    httpContext.TraceIdentifier
                );
            }
            else
            {
                _logger.LogWarning(exception,
                    "Business Exception Occurred! {Message} | Path: {Path} | TraceId: {TraceId}",
                      exception.Message,
                      httpContext.Request.Path,
                      httpContext.TraceIdentifier
                );
            }

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode == StatusCodes.Status500InternalServerError
                    ? "An unexpected error occurred. Please try again later."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
