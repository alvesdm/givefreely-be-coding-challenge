namespace AffiliateService.Application.Services
{
    public interface IEntityServiceBase<TEntity>
    {
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<TEntity>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default);
        IQueryable<TEntity> Query();
        Task<TEntity> InsertAsync(TEntity Affiliate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity Affiliate, Guid uniqueId, CancellationToken cancellationToken = default);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default);
    }
}