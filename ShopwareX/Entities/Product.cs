namespace ShopwareX.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
