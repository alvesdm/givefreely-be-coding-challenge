using AffiliateService.Api.Models;
using AffiliateService.Api.V1.Controllers.Requests;
using AffiliateService.Application.Services;
using AffiliateService.Infrastructure;
using Mapster;
using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests.Handlers
{
    public class CustomerRequestHandler :
        IRequestHandler<GetCustomerRequest, Customer>,
        IRequestHandler<QueryCustomerRequest, PagedResult<Customer>>,
        IRequestHandler<AddCustomerRequest, Customer>,
        IRequestHandler<UpdateCustomerRequest>,
        IRequestHandler<RemoveCustomerRequest>
    {
        private readonly ICustomerService _customerService;
        private readonly IAffiliateService _affiliateService;
        private readonly ILogger<CustomerRequestHandler> _logger;

        public CustomerRequestHandler(
            ICustomerService customerService,
            IAffiliateService affiliateService,
            ILogger<CustomerRequestHandler> logger)
        {
            _customerService = customerService;
            _affiliateService = affiliateService;
            _logger = logger;
        }

        public async Task<Customer> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerService
                .GetAsync(request.UniqueId, cancellationToken);

            if (customer is null)
            {
                throw new NotFoundHttpException("Customer not found.");
            }

            return customer.Adapt<Customer>();
        }

        public async Task<PagedResult<Customer>> Handle(QueryCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _customerService
                .GetAsync(request.Criteria, request.Page, request.PageSize, cancellationToken);

            return new PagedResult<Customer>(result.Item1.Select(p => p.Adapt<Customer>()), result.Item2);
        }

        public async Task<Customer> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
        {
            var result = await _customerService.InsertAsync(await TryFetchNewCustomerEntity(request.Customer, cancellationToken), cancellationToken);

            return result.Adapt<Customer>();
        }

        public async Task Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            await _customerService.UpdateAsync(await TryFetchNewCustomerEntity(request.Customer, cancellationToken), request.UniqueId, cancellationToken);
        }

        public async Task Handle(RemoveCustomerRequest request, CancellationToken cancellationToken)
        {
            await _customerService.RemoveAsync(request.UniqueId, cancellationToken);
        }

        private async Task<Domain.Entities.Customer> TryFetchNewCustomerEntity(InsertUpdateCustomer customer, CancellationToken cancellationToken = default)
        {
            var newCustomer = customer.Adapt<Domain.Entities.Customer>();
            foreach (var a in customer.Affiliates)
            {
                var affiliate = await _affiliateService.GetAsync(a.UniqueId, cancellationToken);
                if (affiliate is null)
                {
                    throw new NotFoundHttpException("One or more affiliates were not found");
                }
                newCustomer.Affiliates.Add(affiliate);
            }

            /// Note: 
            /// this would be a more efficient way in case we'd have to add lots of them as it prevents N+1's
            /// but would also require more code.
            /// Since it's a showcase code, should be fine
            /// 
            //var uniqueIds = customer.Affiliates.Select(a => a.UniqueId);
            //var ids = _affiliateService.Query().Where(a => uniqueIds.Contains(a.UniqueId)).Select(a => a.Id);
            //if(ids.Count() != customer.Affiliates.Count())
            //{
            //    throw new NotFoundHttpException("One or more affiliates were not found");
            //}
            //ids.ToList().ForEach(a => newCustomer.Affiliates.Add(new Domain.Entities.Affiliate { Id = a }));

            return newCustomer;
        }
    }
}