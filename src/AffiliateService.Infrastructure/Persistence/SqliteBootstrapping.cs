using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AffiliateService.Infrastructure.Persistence
{
    public class SqliteBootstrapping : IHostedService
    {
        private readonly IDbContextFactory<AffiliateDbContext> _factory;

        public SqliteBootstrapping(IDbContextFactory<AffiliateDbContext> factory)
        {
            _factory = factory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var context = _factory.CreateDbContext();
            ///Note:
            /// WE are not caring much about column types here as the models are very simple and the auto-generated ones are fine.
            /// Also, in a production lile environment, we would customize each tacble creation either manually or by migation configuration.

            var dbPath = ApplicationConstants.AffiliateServiceDatabasePath;
            if (!File.Exists(dbPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
                using (File.Create(dbPath)) { }
            }

            var result = await context.Database.EnsureCreatedAsync(cancellationToken);

            if (result)
            {
                var affiliate1 = new Domain.Entities.Affiliate
                {
                    Name = "Affiliate 1",
                    UniqueId = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow
                };

                var affiliate2 = new Domain.Entities.Affiliate
                {
                    Name = "Affiliate 2",
                    UniqueId = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow
                };
                var customer1 = new Domain.Entities.Customer
                {
                    Name = "Customer 1",
                    UniqueId = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow
                };
                customer1.Affiliates.AddRange([affiliate1, affiliate2]);

                var customer2 = new Domain.Entities.Customer
                {
                    Name = "Customer 2",
                    UniqueId = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow
                };
                customer2.Affiliates.Add(affiliate1);

                context.Affiliates.AddRange([affiliate1, affiliate2]);
                context.Customers.AddRange([customer1, customer2]);

                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
