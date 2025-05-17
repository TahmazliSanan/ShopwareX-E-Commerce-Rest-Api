using ShopwareX.Dtos.Product;

namespace ShopwareX.Services.Abstracts
{
    public interface IProductService
    {
        Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto);
        Task<ProductResponseDto> GetProductByIdAsync(long id);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> UpdateProductAsync(long id, ProductUpdateDto dto);
        Task<ProductResponseDto> DeleteProductByIdAsync(long id);
    }
}
