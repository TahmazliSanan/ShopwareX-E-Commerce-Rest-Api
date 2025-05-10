using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(p => p.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(p => p.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(170)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .HasColumnType("decimal")
                .HasColumnName("price")
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(p => p.CategoryId)
                .HasColumnType("bigint")
                .HasColumnName("category_id")
                .IsRequired();

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(u => u.CategoryId);

            builder.ToTable("products");
        }
    }
}
