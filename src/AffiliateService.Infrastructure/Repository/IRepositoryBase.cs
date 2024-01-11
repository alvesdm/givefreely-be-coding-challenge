namespace AffiliateService.Infrastructure.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Guid uniqueId);
        Task<Tuple<IEnumerable<T>, int>> GetAsync(string criteria, int page, int pageSize);
        IQueryable<T> Query();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity, Guid uniqueId);
        Task RemoveAsync(int id);
        Task RemoveAsync(Guid uniqueId);
    }
}