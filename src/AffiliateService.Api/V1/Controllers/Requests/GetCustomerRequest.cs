using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class GetCustomerRequest : IRequest<Customer>
    {
        public GetCustomerRequest(Guid uniqueId)
        {
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; }
    }
}