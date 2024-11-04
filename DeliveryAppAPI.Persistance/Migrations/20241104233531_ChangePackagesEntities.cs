using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangePackagesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Countries_CountryId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Dictionaries_TransportationTypeId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_TransportationTypeId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Packages_CountryId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "TransportationTypeId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Packages");

            migrationBuilder.AddColumn<int>(
                name: "TransportationTypeId",
                table: "TransportationItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoragePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PackageId = table.Column<int>(type: "integer", nullable: false),
                    DateOfArrival = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfExit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoragePackages_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationItems_TransportationTypeId",
                table: "TransportationItems",
                column: "TransportationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PackageId",
                table: "Payments",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePackages_PackageId",
                table: "StoragePackages",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Packages_PackageId",
                table: "Payments",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationItems_Dictionaries_TransportationTypeId",
                table: "TransportationItems",
                column: "TransportationTypeId",
                principalTable: "Dictionaries",
                principalColumn: "DictionaryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Packages_PackageId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationItems_Dictionaries_TransportationTypeId",
                table: "TransportationItems");

            migrationBuilder.DropTable(
                name: "StoragePackages");

            migrationBuilder.DropIndex(
                name: "IX_TransportationItems_TransportationTypeId",
                table: "TransportationItems");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PackageId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "TransportationTypeId",
                table: "TransportationItems");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "TransportationTypeId",
                table: "Transportations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Packages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationTypeId",
                table: "Transportations",
                column: "TransportationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CountryId",
                table: "Packages",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Countries_CountryId",
                table: "Packages",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Dictionaries_TransportationTypeId",
                table: "Transportations",
                column: "TransportationTypeId",
                principalTable: "Dictionaries",
                principalColumn: "DictionaryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
