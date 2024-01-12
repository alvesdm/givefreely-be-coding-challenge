using AffiliateService.Infrastructure.Persistence;
using AffiliateService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AffiliateService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IAffiliateRepository, AffiliateRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddDatabases(configuration);

            return services;
        }

        internal static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            //Note: this is for the sake of DB creation convenience.. wouldn't be here in a production-like application!
            var dbPath = ApplicationConstants.AffiliateServiceDatabasePath;
            services.AddDbContextFactory<AffiliateDbContext>(
            options =>
                options.UseSqlite($"Data Source={dbPath}"));

            services.AddHostedService<SqliteBootstrapping>();

            //Note:
            // This is what potentially it would look like in a prod env

            //services.AddDbContextFactory<AffiliateDbContext>(
            //options =>
            //    options.UseSqlServer(configuration.GetConnectionString(ApplicationConstants.AffiliateServiceDatabase)));

            return services;
        }
    }
}
