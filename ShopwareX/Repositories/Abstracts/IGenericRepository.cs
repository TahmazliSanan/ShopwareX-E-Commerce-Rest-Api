using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(long id);
        IQueryable<TEntity> GetAll();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(long id);
        Task SaveAsync();
    }
}
