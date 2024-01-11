using AffiliateService.Domain.Entities;

namespace AffiliateService.Infrastructure.Repository
{
    public interface IAffiliateRepository : IRepositoryBase<Affiliate>
    {
        Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize);
        Task<int> QueryCustomersCountAsync(Guid uniqueId);
    }
}