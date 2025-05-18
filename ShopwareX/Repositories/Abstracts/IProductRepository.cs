using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductByIdAsync(long id);
        IQueryable<Product> GetProductsByIds(IEnumerable<long> ids);
    }
}
