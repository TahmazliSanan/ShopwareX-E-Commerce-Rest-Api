using ShopwareX.Dtos.OrderItem;

namespace ShopwareX.Dtos.Order
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderItemCreateDto> Items { get; set; } = [];
    }
}
