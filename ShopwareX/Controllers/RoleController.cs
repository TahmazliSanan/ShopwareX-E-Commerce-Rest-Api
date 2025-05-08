using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Role;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<RoleResponseDto>>>
            AddRoleAsync([FromBody] RoleCreateDto dto)
        {
            var existRoleByName = await _roleService.GetRoleByNameAsync(dto.Name);
            ApiResponse<RoleResponseDto> apiResponse;

            if (existRoleByName is not null)
            {
                apiResponse = new ApiResponse<RoleResponseDto>
                {
                    Message = "Role already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var newRole = await _roleService.AddRoleAsync(dto);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was created successfully",
                Response = newRole
            };

            return Created($"api/role/{newRole.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RoleResponseDto>>>>
            GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();

            var apiResponse = new ApiResponse<IEnumerable<RoleResponseDto>>
            {
                Message = "All roles were fetched successfully",
                Response = roles
            };

            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<RoleResponseDto>>>
            GetRoleByIdAsync([FromRoute] long id)
        {
            var existRole = await _roleService.GetRoleByIdAsync(id);
            ApiResponse<RoleResponseDto> apiResponse;

            if (existRole is null)
            {
                apiResponse = new ApiResponse<RoleResponseDto>
                {
                    Message = "Role not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was fetched successfully",
                Response = existRole
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<RoleResponseDto>>
            UpdateRoleAsync([FromRoute] long id, [FromBody] RoleUpdateDto dto)
        {
            var existRole = await _roleService.GetRoleByIdAsync(id);
            var existAnotherRoleByName = await _roleService.GetRoleByNameAsync(dto.Name, id);

            ApiResponse<RoleResponseDto> apiResponse;

            if (existRole is null)
            {
                apiResponse = new ApiResponse<RoleResponseDto>
                {
                    Message = "Role not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            if (existAnotherRoleByName is not null)
            {
                apiResponse = new ApiResponse<RoleResponseDto>
                {
                    Message = "Role already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var updatedRole = await _roleService.UpdateRoleAsync(id, dto);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was updated successfully",
                Response = updatedRole
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<RoleResponseDto>> DeleteRoleAsync([FromRoute] long id)
        {
            var existRole = await _roleService.GetRoleByIdAsync(id);
            ApiResponse<RoleResponseDto> apiResponse;

            if (existRole is null)
            {
                apiResponse = new ApiResponse<RoleResponseDto>
                {
                    Message = "Role not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _roleService.DeleteRoleByIdAsync(id);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was deleted successfully",
                Response = existRole
            };

            return Ok(apiResponse);
        }
    }
}
