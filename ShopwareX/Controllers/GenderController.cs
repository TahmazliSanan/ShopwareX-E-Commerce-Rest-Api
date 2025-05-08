using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.Gender;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/gender")]
    [ApiController]
    public class GenderController(IGenderService genderService) : ControllerBase
    {
        private readonly IGenderService _genderService = genderService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>>
            AddGenderAsync([FromBody] GenderCreateDto dto)
        {
            var existGenderByName = await _genderService.GetGenderByNameAsync(dto.Name);
            ApiResponse<GenderResponseDto> apiResponse;

            if (existGenderByName is not null)
            {
                apiResponse = new ApiResponse<GenderResponseDto>
                {
                    Message = "Gender already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var newGender = await _genderService.AddGenderAsync(dto);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was created successfully",
                Response = newGender
            };

            return Created($"api/gender/{newGender.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GenderResponseDto>>>>
            GetAllGendersAsync()
        {
            var genders = await _genderService.GetAllGendersAsync();

            var apiResponse = new ApiResponse<IEnumerable<GenderResponseDto>>
            {
                Message = "All genders were fetched successfully",
                Response = genders
            };

            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>>
            GetGenderByIdAsync([FromRoute] long id)
        {
            var existGender = await _genderService.GetGenderByIdAsync(id);
            ApiResponse<GenderResponseDto> apiResponse;

            if (existGender is null)
            {
                apiResponse = new ApiResponse<GenderResponseDto>
                {
                    Message = "Gender not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was fetched successfully",
                Response = existGender
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<GenderResponseDto>>
            UpdateGenderAsync([FromRoute] long id, [FromBody] GenderUpdateDto dto)
        {
            var existGender = await _genderService.GetGenderByIdAsync(id);
            var existAnotherGenderByName = await _genderService.GetGenderByNameAsync(dto.Name, id);

            ApiResponse<GenderResponseDto> apiResponse;

            if (existGender is null)
            {
                apiResponse = new ApiResponse<GenderResponseDto>
                {
                    Message = "Gender not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            if (existAnotherGenderByName is not null)
            {
                apiResponse = new ApiResponse<GenderResponseDto>
                {
                    Message = "Gender already exists",
                    Response = null
                };

                return Conflict(apiResponse);
            }

            var updatedGender = await _genderService.UpdateGenderAsync(id, dto);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was updated successfully",
                Response = updatedGender
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<GenderResponseDto>> DeleteGenderAsync([FromRoute] long id)
        {
            var existGender = await _genderService.GetGenderByIdAsync(id);
            ApiResponse<GenderResponseDto> apiResponse;

            if (existGender is null)
            {
                apiResponse = new ApiResponse<GenderResponseDto>
                {
                    Message = "Gender not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _genderService.DeleteGenderByIdAsync(id);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was deleted successfully",
                Response = existGender
            };

            return Ok(apiResponse);
        }
    }
}
