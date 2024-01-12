using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class RemoveCustomerRequest : IRequest
    {
        public RemoveCustomerRequest(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }
    }
}