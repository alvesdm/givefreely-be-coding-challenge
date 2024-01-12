using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AffiliateService.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AffiliateDbContext _dbContext;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(
            AffiliateDbContext dbContext,
            ILogger<CustomerRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default) => await _dbContext.Customers.FindAsync(id, cancellationToken);

        public async Task<Customer> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default) => await _dbContext.Customers.Include(e => e.Affiliates).FirstOrDefaultAsync(e => e.UniqueId == uniqueId, cancellationToken);

        public async Task<Tuple<IEnumerable<Customer>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var offset = (page - 1) * pageSize;

            var query = _dbContext.Customers.Include(e => e.Affiliates).Where(e => e.Name.Contains(criteria));
            var count = await query.CountAsync(cancellationToken);
            var totalPages = Math.Ceiling((decimal)count / pageSize);
            if (page > totalPages)
                offset = 0;

            var items = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Tuple<IEnumerable<Customer>, int>(items, count);
        }

        public async Task<Customer> InsertAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }

        public IQueryable<Customer> Query()
        {
            return _dbContext.Customers.AsQueryable();
        }

        public async Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var e = await GetAsync(uniqueId, cancellationToken);
            if (e is null)
            {
                throw new NotFoundHttpException("Customer not found.");
            }
            _dbContext.Customers.Remove(e);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var e = await GetAsync(id, cancellationToken);
            if (e is null)
            {
                throw new NotFoundHttpException("Customer not found.");
            }
            _dbContext.Customers.Remove(e);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Customer Customer, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var current = await GetAsync(uniqueId, cancellationToken);
            if (current is null)
            {
                throw new NotFoundHttpException("Customer not found.");
            }

            current.Name = Customer.Name;

            EFEntityUpdateHelper.Update(current.Affiliates, Customer.Affiliates);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}