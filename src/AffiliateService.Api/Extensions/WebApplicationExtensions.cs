using AffiliateService.Api.Middlewares;

namespace AffiliateService.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigurePresentationMiddlewares(this WebApplication app)
        {
            app.ConfigureErrorhandlingMiddleware();

            return app;
        }
        public static WebApplication ConfigureErrorhandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<UnhandledErrorHandlingMiddleware>();

            return app;
        }
    }
}
