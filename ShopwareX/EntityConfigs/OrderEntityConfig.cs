using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class OrderEntityConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .Property(o => o.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(o => o.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(o => o.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(o => o.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal")
                .HasColumnName("total_price")
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(o => o.UserId)
                .HasColumnType("bigint")
                .HasColumnName("user_id")
                .IsRequired();

            builder
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder.ToTable("orders");
        }
    }
}
