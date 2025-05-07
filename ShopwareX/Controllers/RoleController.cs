using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Role;
using ShopwareX.Entities;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController(IMapper mapper, IRoleService roleService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
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

            var role = _mapper.Map<Role>(dto);
            var newRole = await _roleService.AddRoleAsync(role);
            var roleResponseDto = _mapper.Map<RoleResponseDto>(newRole);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was created successfully",
                Response = roleResponseDto
            };

            return Created($"api/role/{roleResponseDto.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<List<RoleResponseDto>>>> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            var roleResponseDtos = _mapper.Map<List<RoleResponseDto>>(roles);

            var apiResponse = new ApiResponse<List<RoleResponseDto>>
            {
                Message = "All roles were fetched successfully",
                Response = roleResponseDtos
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

            var roleResponseDto = _mapper.Map<RoleResponseDto>(existRole);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was fetched successfully",
                Response = roleResponseDto
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

            var role = _mapper.Map<Role>(dto);
            var updatedRole = await _roleService.UpdateRoleAsync(id, role);
            var roleResponseDto = _mapper.Map<RoleResponseDto>(updatedRole);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was updated successfully",
                Response = roleResponseDto
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
            var roleResponseDto = _mapper.Map<RoleResponseDto>(existRole);

            apiResponse = new ApiResponse<RoleResponseDto>
            {
                Message = "Role was deleted successfully",
                Response = roleResponseDto
            };

            return Ok(apiResponse);
        }
    }
}
