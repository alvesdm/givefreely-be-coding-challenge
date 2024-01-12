using AffiliateService.Infrastructure;
using System.Net;
using System.Text.Json;

namespace AffiliateService.Api.Middlewares
{
    internal class UnhandledErrorHandlingMiddleware
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<UnhandledErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public UnhandledErrorHandlingMiddleware(
            IHostEnvironment hostEnvironment,
            ILogger<UnhandledErrorHandlingMiddleware> logger,
            RequestDelegate next)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured and it's been handled.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status;
            object errors;

            errors = new
            {
                message = exception.Message
            };

            if (exception is BadRequestHttpException e)
            {
                status = (int)HttpStatusCode.BadRequest;
                errors = new
                {
                    message = exception.Message,
                    errors = e.Errors
                };
            }
            else if (exception is NotFoundHttpException)
            {
                status = (int)HttpStatusCode.NotFound;
            }
            else if (exception is NotImplementedHttpException)
            {
                status = (int)HttpStatusCode.NotImplemented;
            }
            else if (exception is UnauthorizedAccessHttpException)
            {
                status = (int)HttpStatusCode.Unauthorized;
            }
            else if(exception is TaskCanceledException)
            {
                status = 499; //Client Closed Request
            }
            else
            {
                status = (int)HttpStatusCode.InternalServerError;

                if (_hostEnvironment.IsProduction())
                {
                    errors = new
                    {
                        message = "An unexpected error happened."
                    };
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(errors));
        }
    }
}
