namespace ShopwareX.Dtos.OrderItem
{
    public class OrderItemResponseDto
    {
        public long Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
