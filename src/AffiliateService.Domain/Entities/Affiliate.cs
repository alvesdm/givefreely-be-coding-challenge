namespace AffiliateService.Domain.Entities
{
    public class Affiliate : IEntity, IUnique
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<Customer> Customers { get; } = [];
    }
}