using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;

namespace AffiliateService.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            services
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                })
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }
    }
}
