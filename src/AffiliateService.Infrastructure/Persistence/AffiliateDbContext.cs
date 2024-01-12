using AffiliateService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AffiliateService.Infrastructure.Persistence
{
    public class AffiliateDbContext : DbContext
    {
        public virtual DbSet<Affiliate> Affiliates { get; set; } = default!;
        public virtual DbSet<Customer> Customers { get; set; } = default!;

        public AffiliateDbContext(DbContextOptions<AffiliateDbContext> options)
            : base(options)
        {
        }

        public AffiliateDbContext()
        {
                
        }

        /// Note:
        /// Disabling LazyLoad to avoid it becoming eager on requests as we dont want navigation properties going deeper by default
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Affiliate>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.Affiliates);
        }
    }
}
