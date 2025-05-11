using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.Category;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        : ICategoryService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto> DeleteCategoryByIdAsync(long id)
        {
            var existCategory = await _categoryRepository.GetCategoryByIdAsync(id);

            if (existCategory is not null)
            {
                existCategory.IsDeleted = true;
                existCategory.UpdatedAt = DateTime.Now;

                existCategory.Products
                    .ToList()
                    .ForEach(p =>
                    {
                        p.IsDeleted = true;
                        p.UpdatedAt = DateTime.Now;
                    });

                await _categoryRepository.SaveAsync();
            }

            return _mapper.Map<CategoryResponseDto>(existCategory);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategories().ToListAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(long id)
        {
            var existCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryResponseDto>(existCategory);
        }

        public async Task<CategoryResponseDto> GetCategoryByNameAsync(string name, long? id = null)
        {
            Category? existCategoryByName;

            if (id is not null)
                existCategoryByName = await _categoryRepository.GetCategoryByNameAsync(name, id);
            else
                existCategoryByName = await _categoryRepository.GetCategoryByNameAsync(name);

            return _mapper.Map<CategoryResponseDto>(existCategoryByName);
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync(long id, CategoryUpdateDto dto)
        {
            var existCategory = await _categoryRepository.GetByIdAsync(id);

            if (existCategory is not null)
            {
                existCategory.Name = dto.Name;

                await _categoryRepository.UpdateAsync(existCategory);
                await _categoryRepository.SaveAsync();
            }

            return _mapper.Map<CategoryResponseDto>(existCategory);
        }
    }
}
