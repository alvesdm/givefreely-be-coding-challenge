namespace AffiliateService.Api.Models
{
    public record InsertUpdateCustomer(string Name, IReadOnlyList<LinkedCustomerToAffiliate> Affiliates);
    public record LinkedCustomerToAffiliate(Guid UniqueId);
    public record Customer(Guid UniqueId, string Name, DateTime DateCreated, IReadOnlyList<Affiliate> Affiliates);
}
