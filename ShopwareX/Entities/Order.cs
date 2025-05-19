namespace ShopwareX.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
