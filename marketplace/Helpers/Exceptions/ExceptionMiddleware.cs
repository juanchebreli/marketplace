using marketplace.Helpers.Exceptions.Implements;
using System.Net;
using System.Text.Json;

namespace marketplace.Helpers.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (error is BaseException exception)
            {
                // base custom exception error
                response.StatusCode = (int)exception.StatusCode;
                var logger = _logger.CreateLogger("LogInformation");
                logger.LogInformation($"{exception}");
            }
            else
            {
                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var logger = _logger.CreateLogger("LogError");
                logger.LogError($"{ResponseMessages.API_ERROR_DEFAULT_LOGGER}: {error}");
            }
                

            var result = new ErrorResponse<object>()
            {
                ErrorCode = response.StatusCode.ToString(),
                ErrorDescription = error.Message ?? ResponseMessages.API_ERROR_INTERNAL_SERVICE,
                Data = error.StackTrace
            };
            
            if (!(error is NoContentException))
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(result).ToString());
            }
        }
    }
}
