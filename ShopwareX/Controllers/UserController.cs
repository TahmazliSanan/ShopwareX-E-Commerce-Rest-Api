using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.User;
using ShopwareX.Entities;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IMapper mapper, IUserService userService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>>
            AddUserAsync([FromBody] UserCreateDto dto)
        {
            var existUserByEmail = await _userService.GetUserByEmailAsync(dto.Email);
            ApiResponse<UserResponseDto> apiResponse;

            if (existUserByEmail is not null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "User already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var user = _mapper.Map<User>(dto);
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = await _userService.AddUserAsync(user);
            var userResponseDto = _mapper.Map<UserResponseDto>(newUser);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was created successfully",
                Response = userResponseDto
            };

            return Created($"api/role/{userResponseDto.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            var userResponseDtos = _mapper.Map<List<UserResponseDto>>(users);

            var apiResponse = new ApiResponse<List<UserResponseDto>>
            {
                Message = "All users were fetched successfully",
                Response = userResponseDtos
            };

            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>>
            GetUserByIdAsync([FromRoute] long id)
        {
            var existUser = await _userService.GetUserByIdAsync(id);
            ApiResponse<UserResponseDto> apiResponse;

            if (existUser is null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "User not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var userResponseDto = _mapper.Map<UserResponseDto>(existUser);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was fetched successfully",
                Response = userResponseDto
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<UserResponseDto>>
            UpdateUserAsync([FromRoute] long id, [FromBody] UserUpdateDto dto)
        {
            var existUser = await _userService.GetUserByIdAsync(id);
            ApiResponse<UserResponseDto> apiResponse;

            if (existUser is null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "User not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var user = _mapper.Map<User>(dto);
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            var userResponseDto = _mapper.Map<UserResponseDto>(updatedUser);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was updated successfully",
                Response = userResponseDto
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<UserResponseDto>> DeleteUserAsync([FromRoute] long id)
        {
            var existUser = await _userService.GetUserByIdAsync(id);
            ApiResponse<UserResponseDto> apiResponse;

            if (existUser is null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "User not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _userService.DeleteUserByIdAsync(id);
            var userResponseDto = _mapper.Map<UserResponseDto>(existUser);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was deleted successfully",
                Response = userResponseDto
            };

            return Ok(apiResponse);
        }
    }
}
