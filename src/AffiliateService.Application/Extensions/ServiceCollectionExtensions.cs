using AffiliateService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AffiliateService.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services
                .AddTransient<IAffiliateService, Services.AffiliateService>()
                .AddTransient<ICustomerService, CustomerService>();

            return services;
        }
    }
}
