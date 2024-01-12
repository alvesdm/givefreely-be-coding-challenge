using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class GetAffiliateCustomersCountReportRequest : PagedQuery<AffiliateCustomersEntryReporting>
    {
        public GetAffiliateCustomersCountReportRequest(int page, int pageSize) : base(page, pageSize)
        { }
    }
}