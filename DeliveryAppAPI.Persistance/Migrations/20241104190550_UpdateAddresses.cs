using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCodeId",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Address",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "PostCodeId",
                table: "Address",
                type: "integer",
                nullable: true);
        }
    }
}
