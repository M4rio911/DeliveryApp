using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class abc1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Transportations",
                schema: "identity",
                newName: "Transportations");

            migrationBuilder.RenameTable(
                name: "TransportationItems",
                schema: "identity",
                newName: "TransportationItems");

            migrationBuilder.RenameTable(
                name: "StoragePackages",
                schema: "identity",
                newName: "StoragePackages");

            migrationBuilder.RenameTable(
                name: "Payments",
                schema: "identity",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Packages",
                schema: "identity",
                newName: "Packages");

            migrationBuilder.RenameTable(
                name: "PackagePrices",
                schema: "identity",
                newName: "PackagePrices");

            migrationBuilder.RenameTable(
                name: "Drivers",
                schema: "identity",
                newName: "Drivers");

            migrationBuilder.RenameTable(
                name: "DictionaryTypes",
                schema: "identity",
                newName: "DictionaryTypes");

            migrationBuilder.RenameTable(
                name: "Dictionaries",
                schema: "identity",
                newName: "Dictionaries");

            migrationBuilder.RenameTable(
                name: "Currencies",
                schema: "identity",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "identity",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Cars",
                schema: "identity",
                newName: "Cars");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "identity",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "identity",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "identity",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "identity",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "identity",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "identity",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "identity",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "identity",
                newName: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Packages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameTable(
                name: "Transportations",
                newName: "Transportations",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "TransportationItems",
                newName: "TransportationItems",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "StoragePackages",
                newName: "StoragePackages",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payments",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Packages",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "PackagePrices",
                newName: "PackagePrices",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Drivers",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "DictionaryTypes",
                newName: "DictionaryTypes",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Dictionaries",
                newName: "Dictionaries",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currencies",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countries",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Cars",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Address",
                newSchema: "identity");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                schema: "identity",
                table: "Packages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
