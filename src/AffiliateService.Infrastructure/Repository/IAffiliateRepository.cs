using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.DTOs;

namespace AffiliateService.Infrastructure.Repository
{
    public interface IAffiliateRepository : IRepositoryBase<Affiliate>
    {
        Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<AffiliateCustomersEntryReportingDTO>, int>> QueryAffiliateCustomersReportingAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}