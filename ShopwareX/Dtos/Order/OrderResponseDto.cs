using ShopwareX.Dtos.OrderItem;

namespace ShopwareX.Dtos.Order
{
    public class OrderResponseDto
    {
        public long Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItemResponseDto> Items { get; set; } = [];
    }
}
