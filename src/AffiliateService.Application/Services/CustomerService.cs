using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace AffiliateService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerRepository repository,
            ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(id, cancellationToken);
        }

        public async Task<Customer> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(uniqueId, cancellationToken);
        }

        public async Task<Tuple<IEnumerable<Customer>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(criteria, page, pageSize, cancellationToken);
        }

        public IQueryable<Customer> Query()
        {
            return _repository.Query();
        }

        public async Task<Customer> InsertAsync(Customer Customer, CancellationToken cancellationToken = default)
        {
            Customer.UniqueId = Guid.NewGuid();
            Customer.DateCreated = DateTime.UtcNow;
            return await _repository.InsertAsync(Customer, cancellationToken);
        }

        public async Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _repository.RemoveAsync(uniqueId, cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.RemoveAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Customer Customer, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(Customer, uniqueId, cancellationToken);
        }
    }
}