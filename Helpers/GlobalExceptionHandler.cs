using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace ourStory.Helpers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Log the exception
            _logger.LogError(exception, exception.Message);

            var response = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = exception.Message
            };

            if (exception is SecurityTokenExpiredException)
            {
                response.Status = (int)HttpStatusCode.Unauthorized;
                response.Title = "Token expired.";
                response.Detail = "Your token has expired. Please log in again.";
            }
            else if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                response.Status = (int)HttpStatusCode.Forbidden;
                response.Title = "Forbidden";
                response.Detail = "You do not have permission to access this resource.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                response.Status = (int)HttpStatusCode.Unauthorized;
                response.Title = "Unauthorized";
                response.Detail = "You are not authorized to access this resource.";
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)response.Status;

            var json = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }
    }
}
