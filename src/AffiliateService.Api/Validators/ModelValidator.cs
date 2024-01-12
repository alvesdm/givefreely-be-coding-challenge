using AffiliateService.Api.Models;
using FluentValidation;

namespace AffiliateService.Api.Validators
{
    public class InsertUpdateAffiliateValidator : AbstractValidator<InsertUpdateAffiliate>
    {
        public InsertUpdateAffiliateValidator()
        {
            RuleFor(x => x.Name).Length(3, 50);
        }
    }

    public class LinkedCustomerToAffiliateValidator : AbstractValidator<LinkedCustomerToAffiliate>
    {
        public LinkedCustomerToAffiliateValidator()
        {
            RuleFor(x => x.UniqueId).NotEmpty();
        }
    }

    public class InsertUpdateCustomerValidator : AbstractValidator<InsertUpdateCustomer>
    {
        public InsertUpdateCustomerValidator()
        {
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}
