namespace AffiliateService.Api.Tests.Integration.Support.Models
{
    public record Affiliate(Guid UniqueId, string Name, DateTime DateCreated, IReadOnlyList<Customer> Customers);
    public record Customer(Guid UniqueId, string Name, DateTime DateCreated, IReadOnlyList<Affiliate> Affiliates);
    public record InsertUpdateAffiliate(string Name);
}
