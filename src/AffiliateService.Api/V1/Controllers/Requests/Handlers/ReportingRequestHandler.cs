using AffiliateService.Api.Models;
using AffiliateService.Application.Services;
using Mapster;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests.Handlers
{
    public class ReportingRequestHandler :
        IRequestHandler<GetAffiliateCustomersCountReportRequest, PagedResult<AffiliateCustomersEntryReporting>>
    {
        private readonly IAffiliateService _affiliateService;
        private readonly ILogger<AffiliateRequestHandler> _logger;

        public ReportingRequestHandler(
            IAffiliateService affiliateService,
            ILogger<AffiliateRequestHandler> logger)
        {
            _affiliateService = affiliateService;
            _logger = logger;
        }

        public async Task<PagedResult<AffiliateCustomersEntryReporting>> Handle(GetAffiliateCustomersCountReportRequest request, CancellationToken cancellationToken)
        {
            var result =  await _affiliateService
                .QueryAffiliateCustomersCountReportAsync(request.Page, request.PageSize, cancellationToken);

            return new PagedResult<AffiliateCustomersEntryReporting>(result.Item1.Select(p=>p.Adapt<AffiliateCustomersEntryReporting>()), result.Item2);
        }
    }
}