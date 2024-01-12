using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.DTOs;

namespace AffiliateService.Application.Services
{
    public interface IAffiliateService : IEntityServiceBase<Affiliate>
    {
        Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<AffiliateCustomersEntryReportingDTO>, int>> QueryAffiliateCustomersCountReportAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}