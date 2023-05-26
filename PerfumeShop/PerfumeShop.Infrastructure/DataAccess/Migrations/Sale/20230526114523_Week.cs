using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Infrastructure.DataAccess.Migrations.Sale
{
    /// <inheritdoc />
    public partial class Week : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Weekends",
                table: "PhysicalShops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weekends",
                table: "PhysicalShops");
        }
    }
}
