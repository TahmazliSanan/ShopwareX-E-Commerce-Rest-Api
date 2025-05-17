using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderByIdAsync(long id);
        IQueryable<Order> GetAllOrders();
    }
}
