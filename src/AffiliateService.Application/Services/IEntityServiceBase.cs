namespace AffiliateService.Application.Services
{
    public interface IEntityServiceBase<TEntity>
    {
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Guid uniqueId);
        Task<Tuple<IEnumerable<TEntity>, int>> GetAsync(string criteria, int page, int pageSize);
        IQueryable<TEntity> Query();
        Task<TEntity> InsertAsync(TEntity Affiliate);
        Task UpdateAsync(TEntity Affiliate, Guid uniqueId);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid uniqueId);
    }
}