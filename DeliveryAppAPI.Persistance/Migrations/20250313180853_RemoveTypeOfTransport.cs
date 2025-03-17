using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTypeOfTransport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationItems_Dictionaries_TransportationTypeId",
                table: "TransportationItems");

            migrationBuilder.DropIndex(
                name: "IX_TransportationItems_TransportationTypeId",
                table: "TransportationItems");

            migrationBuilder.DropColumn(
                name: "TransportationTypeId",
                table: "TransportationItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportationTypeId",
                table: "TransportationItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationItems_TransportationTypeId",
                table: "TransportationItems",
                column: "TransportationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationItems_Dictionaries_TransportationTypeId",
                table: "TransportationItems",
                column: "TransportationTypeId",
                principalTable: "Dictionaries",
                principalColumn: "DictionaryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
