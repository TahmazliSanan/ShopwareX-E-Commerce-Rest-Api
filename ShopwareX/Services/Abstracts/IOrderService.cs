using ShopwareX.Dtos.Order;

namespace ShopwareX.Services.Abstracts
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> AddOrderAsync(OrderCreateDto dto, long userId);
        Task<OrderResponseDto> GetOrderByIdAsync(long id);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto> DeleteOrderByIdAsync(long id);
    }
}
