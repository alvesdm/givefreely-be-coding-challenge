namespace AffiliateService.Infrastructure.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<T> GetAsync(Guid uniqueId, CancellationToken cancellationToken = default);
        Task<Tuple<IEnumerable<T>, int>> GetAsync(string criteria, int page, int pageSize, CancellationToken cancellationToken = default);
        IQueryable<T> Query();
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, Guid uniqueId, CancellationToken cancellationToken = default);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid uniqueId, CancellationToken cancellationToken = default);
    }
}