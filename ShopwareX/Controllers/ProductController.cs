using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Product;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService productService, ICategoryService categoryService)
        : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>>
            AddProductAsync([FromBody] ProductCreateDto dto)
        {
            var existCategory = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);
            ApiResponse<ProductResponseDto> apiResponse;

            if (existCategory is null)
            {
                apiResponse = new ApiResponse<ProductResponseDto>
                {
                    Message = "Category doesn't exist",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var newProduct = await _productService.AddProductAsync(dto);

            apiResponse = new ApiResponse<ProductResponseDto>
            {
                Message = "User was created successfully",
                Response = newProduct
            };

            return Created($"api/product/get/{newProduct.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductResponseDto>>>>
            GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            var apiResponse = new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Message = "All products were fetched successfully",
                Response = products
            };

            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>>
            GetProductByIdAsync([FromRoute] long id)
        {
            var existProduct = await _productService.GetProductByIdAsync(id);
            ApiResponse<ProductResponseDto> apiResponse;

            if (existProduct is null)
            {
                apiResponse = new ApiResponse<ProductResponseDto>
                {
                    Message = "Product not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            apiResponse = new ApiResponse<ProductResponseDto>
            {
                Message = "Product was fetched successfully",
                Response = existProduct
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<ProductResponseDto>>
            UpdateProductAsync([FromRoute] long id, [FromBody] ProductUpdateDto dto)
        {
            var existProduct = await _productService.GetProductByIdAsync(id);
            var existCategory = await _categoryService.GetCategoryByIdAsync(dto.CategoryId);

            ApiResponse<ProductResponseDto> apiResponse;

            if (existProduct is null)
            {
                apiResponse = new ApiResponse<ProductResponseDto>
                {
                    Message = "Product not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            if (existCategory is null)
            {
                apiResponse = new ApiResponse<ProductResponseDto>
                {
                    Message = "Category doesn't exist",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, dto);

            apiResponse = new ApiResponse<ProductResponseDto>
            {
                Message = "Product was updated successfully",
                Response = updatedProduct
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<ProductResponseDto>> DeleteProductAsync([FromRoute] long id)
        {
            var existProduct = await _productService.GetProductByIdAsync(id);
            ApiResponse<ProductResponseDto> apiResponse;

            if (existProduct is null)
            {
                apiResponse = new ApiResponse<ProductResponseDto>
                {
                    Message = "Product not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _productService.DeleteProductByIdAsync(id);

            apiResponse = new ApiResponse<ProductResponseDto>
            {
                Message = "Product was deleted successfully",
                Response = existProduct
            };

            return Ok(apiResponse);
        }
    }
}
