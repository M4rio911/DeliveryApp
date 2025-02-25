using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Drivers_Cars_oneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drivers_AssignedCarId",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "AssignedUserId",
                table: "Cars",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AssignedCarId",
                table: "Drivers",
                column: "AssignedCarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drivers_AssignedCarId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AssignedCarId",
                table: "Drivers",
                column: "AssignedCarId");
        }
    }
}
