using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class AddAffiliateRequest : IRequest<Affiliate>
    {
        public AddAffiliateRequest(InsertUpdateAffiliate affiliate)
        {
            Affiliate = affiliate;
        }

        public InsertUpdateAffiliate Affiliate { get; }
    }
}