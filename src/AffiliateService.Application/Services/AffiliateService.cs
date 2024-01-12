using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.DTOs;
using AffiliateService.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace AffiliateService.Application.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepository _repository;
        private readonly ILogger<AffiliateService> _logger;

        public AffiliateService(
            IAffiliateRepository repository,
            ILogger<AffiliateService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Affiliate> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(id, cancellationToken);
        }

        public async Task<Affiliate> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(uniqueId, cancellationToken);
        }

        public async Task<Tuple<IEnumerable<Affiliate>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _repository.GetAsync(criteria, page, pageSize, cancellationToken);
        }

        public IQueryable<Affiliate> Query()
        {
            return _repository.Query();
        }

        public async Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _repository.QueryCustomersAsync(uniqueId, page, pageSize, cancellationToken);
        }

        public async Task<Affiliate> InsertAsync(Affiliate Affiliate, CancellationToken cancellationToken = default)
        {
            Affiliate.UniqueId = Guid.NewGuid();
            Affiliate.DateCreated = DateTime.UtcNow;
            return await _repository.InsertAsync(Affiliate, cancellationToken);
        }

        public async Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _repository.RemoveAsync(uniqueId, cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.RemoveAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Affiliate Affiliate, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(Affiliate, uniqueId, cancellationToken);
        }

        // ## Reporting ##

        public async Task<Tuple<IEnumerable<AffiliateCustomersEntryReportingDTO>, int>> QueryAffiliateCustomersCountReportAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _repository.QueryAffiliateCustomersReportingAsync(page, pageSize, cancellationToken);
        }
    }
}