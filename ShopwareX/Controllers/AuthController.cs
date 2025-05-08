using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Login;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>>
            LoginUserAsync([FromBody] LoginRequestDto dto)
        {
            var loginResponseDto = await _authService.LoginUserAsync(dto);
            ApiResponse<LoginResponseDto> apiResponse;

            if (loginResponseDto is null)
            {
                apiResponse = new ApiResponse<LoginResponseDto>
                {
                    Message = "Email or password is incorrect",
                    Response = null
                };

                return Unauthorized(apiResponse);
            }

            apiResponse = new ApiResponse<LoginResponseDto>
            {
                Message = "Login is successful",
                Response = loginResponseDto
            };

            return Ok(apiResponse);
        }
    }
}
