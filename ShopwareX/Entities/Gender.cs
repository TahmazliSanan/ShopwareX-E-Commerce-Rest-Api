namespace ShopwareX.Entities
{
    public class Gender : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}
