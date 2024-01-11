namespace AffiliateService.Api.Models
{
    public record InsertUpdateAffiliate(string Name);
    public record Affiliate(Guid UniqueId, string Name, DateTime DateCreated, IReadOnlyList<Customer> Customers);
}
