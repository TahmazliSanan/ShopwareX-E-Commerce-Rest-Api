using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetAllCategories();
        Task<Category?> GetCategoryByIdAsync(long id);
        Task<Category?> GetCategoryByNameAsync(string name, long? id = null);
    }
}
