using AffiliateService.Api.Models;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class QueryAffiliateRequest : QueryRequestBase<Affiliate>
    {
        public QueryAffiliateRequest(string criteria, int page, int pageSize) : base(criteria, page, pageSize) { }
    }
}