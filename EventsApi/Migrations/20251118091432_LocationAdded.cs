using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsApi.Migrations
{
    /// <inheritdoc />
    public partial class LocationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndDate", "Location", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, "Annual technology conference featuring latest innovations", new DateTime(2025, 3, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), null, "Tech Conference 2025", new DateTime(2025, 3, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Interactive workshop to strengthen team collaboration", new DateTime(2025, 2, 20, 17, 0, 0, 0, DateTimeKind.Unspecified), null, "Team Building Workshop", new DateTime(2025, 2, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Exciting launch event for our new product line", new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), null, "Product Launch", new DateTime(2025, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Professional development training for all staff", new DateTime(2025, 1, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), null, "Training Session", new DateTime(2025, 1, 25, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
