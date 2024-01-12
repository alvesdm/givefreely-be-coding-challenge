using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class RemoveAffiliateRequest : IRequest
    {
        public RemoveAffiliateRequest(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }
    }
}