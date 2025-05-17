using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class OrderRepository(AppDbContext context)
        : GenericRepository<Order>(context), IOrderRepository
    {
        private readonly DbSet<Order> _orders = context.Set<Order>();

        public IQueryable<Order> GetAllOrders()
            => _orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .AsQueryable();

        public async Task<Order?> GetOrderByIdAsync(long id)
            => await _orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id && o.IsDeleted == false);
    }
}
