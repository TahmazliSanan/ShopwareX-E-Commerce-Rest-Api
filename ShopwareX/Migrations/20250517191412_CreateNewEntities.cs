using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopwareX.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 17, 23, 14, 11, 532, DateTimeKind.Local).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 17, 23, 14, 11, 532, DateTimeKind.Local).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 17, 23, 14, 11, 535, DateTimeKind.Local).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 17, 23, 14, 11, 535, DateTimeKind.Local).AddTicks(7288));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2025, 5, 17, 23, 14, 11, 535, DateTimeKind.Local).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 17, 23, 14, 11, 700, DateTimeKind.Local).AddTicks(740), "$2a$11$gMkTkWtEgGcV4ma5U9dqzOP1rv.ThGYUNIfMaB6lum5TLw4wFOvU." });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 17, 23, 14, 11, 895, DateTimeKind.Local).AddTicks(2430), "$2a$11$M4Dw39PmEl3Zv6YqTH2OveO4QG/JCq1UZQFAr4GLIhuQeZFgJZ9yy" });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 10, 23, 41, 38, 656, DateTimeKind.Local).AddTicks(6766));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 10, 23, 41, 38, 656, DateTimeKind.Local).AddTicks(6782));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 10, 23, 41, 38, 657, DateTimeKind.Local).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 10, 23, 41, 38, 657, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2025, 5, 10, 23, 41, 38, 657, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 10, 23, 41, 38, 852, DateTimeKind.Local).AddTicks(9610), "$2a$11$4WMKFaB6imPi/doLlGA7D.c8WDhfT6GGJwfUK1S60psoZnlneSL62" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 10, 23, 41, 39, 14, DateTimeKind.Local).AddTicks(4883), "$2a$11$3DJvbLMkQkgd1k.JHVaOvOCais3nL5WydANPBtZDa9R0gm97MSTIC" });
        }
    }
}
