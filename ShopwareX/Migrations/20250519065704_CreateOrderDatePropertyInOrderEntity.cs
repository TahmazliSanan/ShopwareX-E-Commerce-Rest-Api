using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopwareX.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderDatePropertyInOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 10, 57, 3, 504, DateTimeKind.Local).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 10, 57, 3, 504, DateTimeKind.Local).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 10, 57, 3, 507, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 10, 57, 3, 507, DateTimeKind.Local).AddTicks(2921));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "created_at",
                value: new DateTime(2025, 5, 19, 10, 57, 3, 507, DateTimeKind.Local).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 19, 10, 57, 3, 663, DateTimeKind.Local).AddTicks(3770), "$2a$11$tfPLHU.MVn5afYNSGkkY6elM0d1cB1n2.ZJGozpaASwVT/ClywVbW" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "hashed_password" },
                values: new object[] { new DateTime(2025, 5, 19, 10, 57, 3, 841, DateTimeKind.Local).AddTicks(2585), "$2a$11$wQzDB4vxRq0nQJi7ti5zFuKjkOuxRxXQR2Oh..XRpEKLxpjDcCakG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "orders");

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
        }
    }
}
