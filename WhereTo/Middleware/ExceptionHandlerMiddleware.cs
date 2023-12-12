using System.Net;
using System.Text.Json;

namespace WhereTo.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string message = string.Empty;
            HttpStatusCode statusCode;

            // Handle different types of exceptions 
            (message, statusCode) = ex switch
            {
                KeyNotFoundException keyNotFound => ("The requested resource was not found.", HttpStatusCode.NotFound),
                _ => ("An error occurred. Please try again later.", HttpStatusCode.InternalServerError)
            };

            response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(new { error = message });
            logger.LogError(message);
            await response.WriteAsync(result);
        }
    }    
}
