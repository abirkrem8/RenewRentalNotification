using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewRentalNotification.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaseDocument",
                table: "Tenants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "LeaseDocument",
                table: "Tenants",
                type: "longblob",
                nullable: false);
        }
    }
}
