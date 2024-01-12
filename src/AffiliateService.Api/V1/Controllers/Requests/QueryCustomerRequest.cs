using AffiliateService.Api.Models;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class QueryCustomerRequest : QueryRequestBase<Customer>
    {
        public QueryCustomerRequest(string criteria, int page, int pageSize) : base(criteria, page, pageSize) { }
    }
}