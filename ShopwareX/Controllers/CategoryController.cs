using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.Category;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Role;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<RoleResponseDto>>>
            AddCategoryAsync([FromBody] CategoryCreateDto dto)
        {
            var existCategoryByName = await _categoryService.GetCategoryByNameAsync(dto.Name);
            ApiResponse<CategoryResponseDto> apiResponse;

            if (existCategoryByName is not null)
            {
                apiResponse = new ApiResponse<CategoryResponseDto>
                {
                    Message = "Category already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var newCategory = await _categoryService.AddCategoryAsync(dto);

            apiResponse = new ApiResponse<CategoryResponseDto>
            {
                Message = "Category was created successfully",
                Response = newCategory
            };

            return Created($"api/category/{newCategory.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryResponseDto>>>>
            GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var apiResponse = new ApiResponse<IEnumerable<CategoryResponseDto>>
            {
                Message = "All categories were fetched successfully",
                Response = categories
            };

            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>>
            GetCategoryByIdAsync([FromRoute] long id)
        {
            var existCategory = await _categoryService.GetCategoryByIdAsync(id);
            ApiResponse<CategoryResponseDto> apiResponse;

            if (existCategory is null)
            {
                apiResponse = new ApiResponse<CategoryResponseDto>
                {
                    Message = "Category not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            apiResponse = new ApiResponse<CategoryResponseDto>
            {
                Message = "Category was fetched successfully",
                Response = existCategory
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<CategoryResponseDto>>
            UpdateCategoryAsync([FromRoute] long id, [FromBody] CategoryUpdateDto dto)
        {
            var existCategory = await _categoryService.GetCategoryByIdAsync(id);
            var existAnotherCategoryByName = await _categoryService.GetCategoryByNameAsync(dto.Name, id);

            ApiResponse<CategoryResponseDto> apiResponse;

            if (existCategory is null)
            {
                apiResponse = new ApiResponse<CategoryResponseDto>
                {
                    Message = "Category not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            if (existAnotherCategoryByName is not null)
            {
                apiResponse = new ApiResponse<CategoryResponseDto>
                {
                    Message = "Category already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, dto);

            apiResponse = new ApiResponse<CategoryResponseDto>
            {
                Message = "Category was updated successfully",
                Response = updatedCategory
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<CategoryResponseDto>> DeleteCategoryAsync([FromRoute] long id)
        {
            var existCategory = await _categoryService.GetCategoryByIdAsync(id);
            ApiResponse<CategoryResponseDto> apiResponse;

            if (existCategory is null)
            {
                apiResponse = new ApiResponse<CategoryResponseDto>
                {
                    Message = "Category not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _categoryService.DeleteCategoryByIdAsync(id);

            apiResponse = new ApiResponse<CategoryResponseDto>
            {
                Message = "Category was deleted successfully",
                Response = existCategory
            };

            return Ok(apiResponse);
        }
    }
}
