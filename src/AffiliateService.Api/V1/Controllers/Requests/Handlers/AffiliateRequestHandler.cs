using AffiliateService.Api.Models;
using AffiliateService.Application.Services;
using AffiliateService.Infrastructure;
using Mapster;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests.Handlers
{
    public class AffiliateRequestHandler :
        IRequestHandler<GetAffiliateRequest, Affiliate>,
        IRequestHandler<QueryAffiliateRequest, PagedResult<Affiliate>>,
        IRequestHandler<AddAffiliateRequest, Affiliate>,
        IRequestHandler<UpdateAffiliateRequest>,
        IRequestHandler<RemoveAffiliateRequest>,
        IRequestHandler<QueryAffiliateCustomersRequest, PagedResult<Customer>>


    {
        private readonly IAffiliateService _affiliateService;
        private readonly ILogger<AffiliateRequestHandler> _logger;

        public AffiliateRequestHandler(
            IAffiliateService affiliateService,
            ILogger<AffiliateRequestHandler> logger)
        {
            _affiliateService = affiliateService;
            _logger = logger;
        }

        public async Task<Affiliate> Handle(GetAffiliateRequest request, CancellationToken cancellationToken)
        {
            var affiliate = await _affiliateService
                .GetAsync(request.UniqueId, cancellationToken);

            if (affiliate is null)
            {
                throw new NotFoundHttpException("Affiliate not found.");
            }

            return affiliate.Adapt<Affiliate>();
        }

        public async Task<PagedResult<Affiliate>> Handle(QueryAffiliateRequest request, CancellationToken cancellationToken)
        {
            var result = await _affiliateService
                .GetAsync(request.Criteria, request.Page, request.PageSize, cancellationToken);

            return new PagedResult<Affiliate>(result.Item1.Select(p => p.Adapt<Affiliate>()), result.Item2);
        }

        public async Task<Affiliate> Handle(AddAffiliateRequest request, CancellationToken cancellationToken)
        {
            var result = await _affiliateService.InsertAsync(request.Affiliate.Adapt<Domain.Entities.Affiliate>(), cancellationToken);

            return result.Adapt<Affiliate>();
        }

        public async Task Handle(UpdateAffiliateRequest request, CancellationToken cancellationToken)
        {
            await _affiliateService.UpdateAsync(request.Affiliate.Adapt<Domain.Entities.Affiliate>(), request.UniqueId, cancellationToken);
        }

        public async Task Handle(RemoveAffiliateRequest request, CancellationToken cancellationToken)
        {
            await _affiliateService.RemoveAsync(request.UniqueId, cancellationToken);
        }

        public async Task<PagedResult<Customer>> Handle(QueryAffiliateCustomersRequest request, CancellationToken cancellationToken)
        {
            var result = await _affiliateService
                .QueryCustomersAsync(request.UniqueId, request.Page, request.PageSize, cancellationToken);

            return new PagedResult<Customer>(result.Item1.Select(p => p.Adapt<Customer>()), result.Item2);
        }
    }
}