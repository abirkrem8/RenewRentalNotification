using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewRentalNotification.Migrations
{
    /// <inheritdoc />
    public partial class PropertyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_RentalProperties_Id",
                table: "Tenants");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalPropertyId",
                table: "Tenants",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_RentalPropertyId",
                table: "Tenants",
                column: "RentalPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_RentalProperties_RentalPropertyId",
                table: "Tenants",
                column: "RentalPropertyId",
                principalTable: "RentalProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_RentalProperties_RentalPropertyId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_RentalPropertyId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "RentalPropertyId",
                table: "Tenants");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_RentalProperties_Id",
                table: "Tenants",
                column: "Id",
                principalTable: "RentalProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
