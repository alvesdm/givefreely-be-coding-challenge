using AffiliateService.Domain.Entities;

namespace AffiliateService.Infrastructure.Tests.Unit
{
    internal static class TestDataHelper
    {
        internal static class Affiliates
        {
            private static List<Affiliate> _affiliates = new List<Affiliate>
                {
                    new Affiliate { Id = 1, UniqueId = Guid.NewGuid(), Name = "Affiliate 1", DateCreated = DateTime.UtcNow.AddDays(-1) },
                    new Affiliate { Id = 2, UniqueId = Guid.NewGuid(), Name = "Affiliate 2", DateCreated = DateTime.UtcNow.AddDays(-2) },
                    new Affiliate { Id = 3, UniqueId = Guid.NewGuid(), Name = "Affiliate 3", DateCreated = DateTime.UtcNow.AddDays(-3) },
                };
            internal static List<Affiliate> GetFakeList()
            {
                _affiliates.First().Customers.AddRange(Customers.GetFakeList().Take(2));

                return _affiliates;
            }
        }

        internal static class Customers
        {
            private static List<Customer> _customers = new List<Customer>
                {
                    new Customer { Id = 1, UniqueId = Guid.NewGuid(), Name = "Customer 1", DateCreated = DateTime.UtcNow.AddDays(-1) },
                    new Customer { Id = 2, UniqueId = Guid.NewGuid(), Name = "Customer 2", DateCreated = DateTime.UtcNow.AddDays(-2) },
                    new Customer { Id = 3, UniqueId = Guid.NewGuid(), Name = "Customer 3", DateCreated = DateTime.UtcNow.AddDays(-3) },
                };
            internal static List<Customer> GetFakeList()
            {
                return _customers;
            }
        }
    }
}