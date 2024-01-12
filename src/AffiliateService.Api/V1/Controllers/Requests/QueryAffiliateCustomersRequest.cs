using AffiliateService.Api.Models;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class QueryAffiliateCustomersRequest : PagedQuery<Customer>
    {
        public QueryAffiliateCustomersRequest(Guid uniqueId, int page, int pageSize) : base(page, pageSize)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }
    }
}