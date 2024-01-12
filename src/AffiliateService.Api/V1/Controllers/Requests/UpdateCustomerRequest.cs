using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class UpdateCustomerRequest : IRequest
    {
        public UpdateCustomerRequest(InsertUpdateCustomer customer, Guid uniqueId)
        {
            Customer = customer;
            UniqueId = uniqueId;
        }

        public InsertUpdateCustomer Customer { get; }
        public Guid UniqueId { get; }
    }
}