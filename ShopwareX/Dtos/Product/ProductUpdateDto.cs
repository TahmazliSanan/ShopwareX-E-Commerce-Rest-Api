namespace ShopwareX.Dtos.Product
{
    public class ProductUpdateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
    }
}
