using ShopwareX.Dtos.Category;

namespace ShopwareX.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto);
        Task<CategoryResponseDto> GetCategoryByIdAsync(long id);
        Task<CategoryResponseDto> GetCategoryByNameAsync(string name, long? id = null);
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> UpdateCategoryAsync(long id, CategoryUpdateDto dto);
        Task<CategoryResponseDto> DeleteCategoryByIdAsync(long id);
    }
}
