using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.User;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService, IGenderService genderService)
        : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IGenderService _genderService = genderService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>>
            AddUserAsync([FromBody] UserCreateDto dto)
        {
            var existUserByEmail = await _userService.GetUserByEmailAsync(dto.Email);
            var existGender = await _genderService.GetGenderByIdAsync(dto.GenderId);

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

            if (existGender is null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "Gender doesn't exist",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var newUser = await _userService.AddUserAsync(dto);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was created successfully",
                Response = newUser
            };

            return Created($"api/user/{newUser.Id}", apiResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>>
            GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            var apiResponse = new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Message = "All users were fetched successfully",
                Response = users
            };

            return Ok(apiResponse);
        }

        [Authorize(Roles = "Admin, User")]
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

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was fetched successfully",
                Response = existUser
            };

            return Ok(apiResponse);
        }

        [Authorize(Roles = "User")]
        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<UserResponseDto>>
            UpdateUserAsync([FromRoute] long id, [FromBody] UserUpdateDto dto)
        {
            var existUser = await _userService.GetUserByIdAsync(id);
            var existGender = await _genderService.GetGenderByIdAsync(dto.GenderId);

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

            if (existGender is null)
            {
                apiResponse = new ApiResponse<UserResponseDto>
                {
                    Message = "Gender doesn't exist",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            var updatedUser = await _userService.UpdateUserAsync(id, dto);

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was updated successfully",
                Response = updatedUser
            };

            return Ok(apiResponse);
        }

        [Authorize(Roles = "User")]
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

            apiResponse = new ApiResponse<UserResponseDto>
            {
                Message = "User was deleted successfully",
                Response = existUser
            };

            return Ok(apiResponse);
        }
    }
}
