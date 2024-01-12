using AffiliateService.Api.Models;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class AddCustomerRequest : IRequest<Customer>
    {
        public AddCustomerRequest(InsertUpdateCustomer customer)
        {
            Customer = customer;
        }

        public InsertUpdateCustomer Customer { get; }
    }
}