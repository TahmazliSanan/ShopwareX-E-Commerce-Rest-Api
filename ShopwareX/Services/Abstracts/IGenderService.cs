using ShopwareX.Dtos.Gender;

namespace ShopwareX.Services.Abstracts
{
    public interface IGenderService
    {
        Task<GenderResponseDto> AddGenderAsync(GenderCreateDto dto);
        Task<GenderResponseDto> GetGenderByIdAsync(long id);
        Task<GenderResponseDto> GetGenderByNameAsync(string name, long? id = null);
        Task<IEnumerable<GenderResponseDto>> GetAllGendersAsync();
        Task<GenderResponseDto> UpdateGenderAsync(long id, GenderUpdateDto dto);
        Task<GenderResponseDto> DeleteGenderByIdAsync(long id);
    }
}
