using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(u => u.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(u => u.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(u => u.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(170)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(u => u.HashedPassword)
                .HasColumnName("hashed_password")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(u => u.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");

            builder
                .Property(u => u.GenderId)
                .HasColumnType("bigint")
                .HasColumnName("gender_id")
                .IsRequired();

            builder
                .HasOne(u => u.Gender)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GenderId);

            builder
                .Property(u => u.RoleId)
                .HasColumnType("bigint")
                .HasColumnName("role_id")
                .IsRequired();

            builder
                .HasOne(u => u.Role)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.RoleId);

            builder.HasData(
                new User
                {
                    Id = 1,
                    FullName = "Super Admin",
                    Email = "super.admin@example.com",
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("super@admin123"),
                    GenderId = 1,
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FullName = "Admin",
                    Email = "admin@example.com",
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    GenderId = 1,
                    RoleId = 2,
                });

            builder.ToTable("users");
        }
    }
}
