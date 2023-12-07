using System.Net;
using System.Text.Json;

namespace WhereTo.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
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
            string message;
            HttpStatusCode statusCode;

            // Handle different types of exceptions 
            switch (ex)
            {
                case KeyNotFoundException keyNotFound:
                    message = "The requested resource was not found.";
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    message = "An error occurred. Please try again later.";
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(new { error = message });
            await response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
