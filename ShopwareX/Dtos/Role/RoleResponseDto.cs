using ShopwareX.Dtos.User;

namespace ShopwareX.Dtos.Role
{
    public class RoleResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<UserResponseDto> Users { get; set; } = [];
    }
}
