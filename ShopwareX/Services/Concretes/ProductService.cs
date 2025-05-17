using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.Product;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class ProductService(IMapper mapper, IProductRepository productRepository)
        : IProductService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> DeleteProductByIdAsync(long id)
        {
            var existProduct = await _productRepository.GetByIdAsync(id);

            if (existProduct is not null)
            {
                await _productRepository.DeleteByIdAsync(id);
                await _productRepository.SaveAsync();
            }

            return _mapper.Map<ProductResponseDto>(existProduct);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(long id)
        {
            var existProduct = await _productRepository.GetProductByIdAsync(id);
            return _mapper.Map<ProductResponseDto>(existProduct);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(long id, ProductUpdateDto dto)
        {
            var existProduct = await _productRepository.GetByIdAsync(id);

            if (existProduct is not null)
            {
                existProduct.Name = dto.Name;
                existProduct.Price = dto.Price;
                existProduct.CategoryId = dto.CategoryId;

                await _productRepository.UpdateAsync(existProduct);
                await _productRepository.SaveAsync();
            }

            return _mapper.Map<ProductResponseDto>(existProduct);
        }
    }
}
