using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class UpdateAffiliateRequest : IRequest
    {
        public UpdateAffiliateRequest(InsertUpdateAffiliate affiliate, Guid uniqueId)
        {
            Affiliate = affiliate;
            UniqueId = uniqueId;
        }

        public InsertUpdateAffiliate Affiliate { get; }
        public Guid UniqueId { get; }
    }
}