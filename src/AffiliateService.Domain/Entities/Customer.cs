namespace AffiliateService.Domain.Entities
{
    public class Customer : IEntity, IUnique
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<Affiliate> Affiliates { get; } = [];
    }
}