using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class CategoryEntityConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(c => c.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(c => c.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(170)
                .IsRequired();

            builder.ToTable("categories");
        }
    }
}
