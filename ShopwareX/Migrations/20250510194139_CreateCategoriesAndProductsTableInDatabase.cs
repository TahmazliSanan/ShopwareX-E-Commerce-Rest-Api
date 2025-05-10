using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopwareX.Migrations
{
    /// <inheritdoc />
    public partial class CreateCategoriesAndProductsTableInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(170)", maxLength: 170, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(170)", maxLength: 170, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 13, 3, 34, DateTimeKind.Local).AddTicks(763));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 13, 3, 34, DateTimeKind.Local).AddTicks(783));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 13, 3, 34, DateTimeKind.Local).AddTicks(8005));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 13, 3, 34, DateTimeKind.Local).AddTicks(8015));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2025, 5, 9, 15, 13, 3, 34, DateTimeKind.Local).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 9, 15, 13, 3, 199, DateTimeKind.Local).AddTicks(1210), "$2a$11$BLgnjqwirV.7woRYI9grg.0QF/bTteHZUFtO8Yjg2FAUTbnKdC.Ii" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 9, 15, 13, 3, 358, DateTimeKind.Local).AddTicks(6390), "$2a$11$r5WulxO4lyU1kajrs30/PObaZPZOn9Vz7EVVhusdWdn2tNbN/20ly" });
        }
    }
}
