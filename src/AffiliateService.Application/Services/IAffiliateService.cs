using AffiliateService.Domain.Entities;

namespace AffiliateService.Application.Services
{
    public interface IAffiliateService : IEntityServiceBase<Affiliate>
    {
        Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize);
        Task<int> QueryCustomersCountAsync(Guid uniqueId);
    }
}