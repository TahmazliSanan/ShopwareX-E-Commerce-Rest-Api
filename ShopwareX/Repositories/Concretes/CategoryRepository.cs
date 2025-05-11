using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class CategoryRepository(AppDbContext context)
        : GenericRepository<Category>(context), ICategoryRepository
    {
        private readonly DbSet<Category> _categories = context.Set<Category>();

        public IQueryable<Category> GetAllCategories()
            => _categories
            .Include(c => c.Products)
            .AsQueryable();

        public async Task<Category?> GetCategoryByIdAsync(long id)
            => await _categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);

        public async Task<Category?> GetCategoryByNameAsync(string name, long? id = null)
        {
            if (id is not null)
                return await _categories
                .FirstOrDefaultAsync(c => c.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && c.Id != id && c.IsDeleted == false);

            return await _categories
                .FirstOrDefaultAsync(c => c.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && c.IsDeleted == false);
        }
    }
}
