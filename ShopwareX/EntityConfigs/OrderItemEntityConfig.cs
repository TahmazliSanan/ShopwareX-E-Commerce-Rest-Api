using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopwareX.Entities;

namespace ShopwareX.EntityConfigs
{
    public class OrderItemEntityConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder
                .Property(oi => oi.Id)
                .HasColumnType("bigint")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(oi => oi.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at")
                .IsRequired();

            builder
                .Property(oi => oi.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            builder
                .Property(oi => oi.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder
                .Property(oi => oi.Quantity)
                .HasColumnType("bigint")
                .HasColumnName("quantity")
                .IsRequired();

            builder
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal")
                .HasColumnName("unit_price")
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(oi => oi.OrderId)
                .HasColumnType("bigint")
                .HasColumnName("order_id")
                .IsRequired();

            builder
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            builder
                .Property(oi => oi.ProductId)
                .HasColumnType("bigint")
                .HasColumnName("product_id")
                .IsRequired();

            builder
                .HasOne(oi => oi.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(oi => oi.ProductId);

            builder.ToTable("order_items");
        }
    }
}
