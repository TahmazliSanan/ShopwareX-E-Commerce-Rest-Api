using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class ProductRepository(AppDbContext context)
        : GenericRepository<Product>(context), IProductRepository
    {
        private readonly DbSet<Product> _products = context.Set<Product>();

        public async Task<Product?> GetProductByIdAsync(long id)
            => await _products
            .Include(u => u.Category)
            .FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);

        public IQueryable<Product> GetProductsByIds(IEnumerable<long> ids)
            => _products
                .Where(p => ids.Contains(p.Id))
                .AsQueryable();
    }
}
