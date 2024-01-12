using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffiliateService.Domain.Entities
{
    [Table("Customers")]
    public class Customer : IEntity, IUnique
    {
        [Key]
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<Affiliate> Affiliates { get; } = [];
    }
}