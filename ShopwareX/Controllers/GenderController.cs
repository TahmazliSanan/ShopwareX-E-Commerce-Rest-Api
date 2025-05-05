using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.Gender;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Entities;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/gender")]
    [ApiController]
    public class GenderController(IMapper mapper, IGenderService genderService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IGenderService _genderService = genderService;

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>>
            AddGenderAsync([FromBody] GenderCreateDto dto)
        {
            var gender = _mapper.Map<Gender>(dto);
            var newGender = await _genderService.AddAsync(gender);
            var genderResponseDto = _mapper.Map<GenderResponseDto>(newGender);

            var apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was created successfully",
                Response = genderResponseDto
            };

            return Created($"api/gender/{genderResponseDto.Id}", apiResponse);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<List<GenderResponseDto>>>> GetAllGendersAsync()
        {
            var genders = await _genderService.GetAllAsync();
            var genderResponseDtos = _mapper.Map<List<GenderResponseDto>>(genders);
            
            var apiResponse = new ApiResponse<List<GenderResponseDto>>
            {
                Message = "All genders were fetched successfully",
                Response = genderResponseDtos
            };
            
            return Ok(apiResponse);
        }

        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<GenderResponseDto>>>
            GetGenderByIdAsync([FromRoute] long id)
        {
            var existGender = await _genderService.GetByIdAsync(id);
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

            var genderResponseDto = _mapper.Map<GenderResponseDto>(existGender);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was fetched successfully",
                Response = genderResponseDto
            };

            return Ok(apiResponse);
        }

        [HttpPut("update/{id:long}")]
        public async Task<ActionResult<GenderResponseDto>>
            UpdateGenderAsync([FromRoute] long id, [FromBody] GenderUpdateDto dto)
        {
            var existGender = await _genderService.GetByIdAsync(id);
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

            var gender = _mapper.Map<Gender>(dto);
            var updatedGender = await _genderService.UpdateAsync(id, gender);
            var genderResponseDto = _mapper.Map<GenderResponseDto>(updatedGender);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was updated successfully",
                Response = genderResponseDto
            };

            return Ok(apiResponse);
        }

        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<GenderResponseDto>> DeleteGender([FromRoute] long id)
        {
            var existGender = await _genderService.GetByIdAsync(id);
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

            await _genderService.DeleteAsync(id);
            var genderResponseDto = _mapper.Map<GenderResponseDto>(existGender);

            apiResponse = new ApiResponse<GenderResponseDto>
            {
                Message = "Gender was deleted successfully",
                Response = genderResponseDto
            };

            return Ok(apiResponse);
        }
    }
}
