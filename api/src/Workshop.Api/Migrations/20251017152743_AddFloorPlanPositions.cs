using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshop.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFloorPlanPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionX",
                table: "ReservableObjects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionY",
                table: "ReservableObjects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 2 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 5 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 2, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 5, 8 });

            migrationBuilder.UpdateData(
                table: "ReservableObjects",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "PositionX", "PositionY" },
                values: new object[] { 8, 8 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "ReservableObjects");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "ReservableObjects");
        }
    }
}
