using AffiliateService.Domain.Entities;
using AffiliateService.Infrastructure.DTOs;
using AffiliateService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AffiliateService.Infrastructure.Repository
{
    public class AffiliateRepository : IAffiliateRepository
    {
        private readonly AffiliateDbContext _dbContext;
        private readonly ILogger<AffiliateRepository> _logger;

        public AffiliateRepository(
            AffiliateDbContext dbContext,
            ILogger<AffiliateRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Affiliate> GetAsync(int id, CancellationToken cancellationToken = default) => await _dbContext.Affiliates.FindAsync(id, cancellationToken);

        public async Task<Affiliate> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default) => await _dbContext.Affiliates.Include(e => e.Customers).FirstOrDefaultAsync(e => e.UniqueId == uniqueId, cancellationToken);

        public async Task<Tuple<IEnumerable<Affiliate>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var offset = (page - 1) * pageSize;

            var query = _dbContext.Affiliates.Include(e => e.Customers).Where(e => e.Name.Contains(criteria));
            var count = await query.CountAsync(cancellationToken);
            var totalPages = Math.Ceiling((decimal)count / pageSize);
            if(page > totalPages)
                offset = 0;

            var items = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Tuple<IEnumerable<Affiliate>, int>(items, count);
        }

        public async Task<Affiliate> InsertAsync(Affiliate affiliate, CancellationToken cancellationToken = default)
        {
            _dbContext.Affiliates.Add(affiliate);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return affiliate;
        }

        public IQueryable<Affiliate> Query()
        {
            return _dbContext.Affiliates.AsQueryable();
        }

        public async Task<Tuple<IEnumerable<Customer>, int>> QueryCustomersAsync(Guid uniqueId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var offset = (page - 1) * pageSize;

            var query = _dbContext.Customers.Where(c => c.Affiliates.Any(a => a.UniqueId == uniqueId));
            var count = await query.CountAsync(cancellationToken);
            var totalPages = Math.Ceiling((decimal)count / pageSize);
            if (page > totalPages)
                offset = 0;

            var items = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Tuple<IEnumerable<Customer>, int>(items, count);
        }

        public async Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var e = await GetAsync(uniqueId, cancellationToken);
            if (e is null)
            {
                throw new NotFoundHttpException("Affiliate not found.");
            }
            _dbContext.Affiliates.Remove(e);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var e = await GetAsync(id, cancellationToken);
            if (e is null)
            {
                throw new NotFoundHttpException("Affiliate not found.");
            }
            _dbContext.Affiliates.Remove(e);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Affiliate affiliate, Guid uniqueId, CancellationToken cancellationToken = default)
        {
            var current = await GetAsync(uniqueId, cancellationToken);
            if (current is null)
            {
                throw new NotFoundHttpException("Affiliate not found.");
            }

            current.Name = affiliate.Name;

            ///Note:
            /// We are not allowing customers to be updated
            //EFEntityUpdateHelper.Update(current.Customers, affiliate.Customers);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // ## Reporting ##
        
        public async Task<Tuple<IEnumerable<AffiliateCustomersEntryReportingDTO>, int>> QueryAffiliateCustomersReportingAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var offset = (page - 1) * pageSize;

            var query = _dbContext.Affiliates.Select(a => new AffiliateCustomersEntryReportingDTO(a.UniqueId, a.Name, a.Customers.Count()));
            var count = await query.CountAsync(cancellationToken);
            var totalPages = Math.Ceiling((decimal)count / pageSize);
            if (page > totalPages)
                offset = 0;

            var items = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new Tuple<IEnumerable<AffiliateCustomersEntryReportingDTO>, int>(items, count);
        }
    }
}