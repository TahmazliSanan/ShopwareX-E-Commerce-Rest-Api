using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(r => r.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(r => r.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(r => r.Name)
                .HasColumnName("name")
                .HasMaxLength(170)
                .IsRequired();

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Super Admin",
                    CreatedAt = DateTime.Now
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin",
                    CreatedAt = DateTime.Now
                },
                new Role
                {
                    Id = 3,
                    Name = "User",
                    CreatedAt = DateTime.Now
                });

            builder.ToTable("roles");
        }
    }
}
