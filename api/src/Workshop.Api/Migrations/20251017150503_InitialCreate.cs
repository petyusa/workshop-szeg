using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workshop.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservableObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservableObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservableObjects_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "123 Main Street, Downtown", true, "Downtown Location" },
                    { 2, "456 North Avenue", true, "North Branch" },
                    { 3, "789 East Boulevard", true, "East Side Location" }
                });

            migrationBuilder.InsertData(
                table: "ReservableObjects",
                columns: new[] { "Id", "IsAvailable", "LocationId", "Name", "Type" },
                values: new object[,]
                {
                    { 1, true, 1, "Desk 101", 0 },
                    { 2, false, 1, "Desk 201", 0 },
                    { 3, true, 1, "Desk 301", 0 },
                    { 4, false, 1, "Desk 401", 0 },
                    { 5, true, 1, "Desk 501", 0 },
                    { 6, true, 1, "Parking A-1", 1 },
                    { 7, false, 1, "Parking A-2", 1 },
                    { 8, true, 1, "Parking A-3", 1 },
                    { 9, false, 2, "Desk 102", 0 },
                    { 10, true, 2, "Desk 202", 0 },
                    { 11, false, 2, "Desk 302", 0 },
                    { 12, true, 2, "Desk 402", 0 },
                    { 13, true, 2, "Desk 502", 0 },
                    { 14, false, 2, "Parking B-1", 1 },
                    { 15, true, 2, "Parking B-2", 1 },
                    { 16, true, 2, "Parking B-3", 1 },
                    { 17, true, 3, "Desk 103", 0 },
                    { 18, true, 3, "Desk 203", 0 },
                    { 19, true, 3, "Desk 303", 0 },
                    { 20, true, 3, "Desk 403", 0 },
                    { 21, false, 3, "Desk 503", 0 },
                    { 22, true, 3, "Parking C-1", 1 },
                    { 23, true, 3, "Parking C-2", 1 },
                    { 24, true, 3, "Parking C-3", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservableObjects_LocationId",
                table: "ReservableObjects",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservableObjects");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
