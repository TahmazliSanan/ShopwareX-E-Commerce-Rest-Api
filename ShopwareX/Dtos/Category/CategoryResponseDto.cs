using ShopwareX.Dtos.Product;

namespace ShopwareX.Dtos.Category
{
    public class CategoryResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<ProductResponseDto> Products { get; set; } = [];
    }
}
