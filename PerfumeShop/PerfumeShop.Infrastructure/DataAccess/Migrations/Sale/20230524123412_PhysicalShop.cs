using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Infrastructure.DataAccess.Migrations.Sale
{
    /// <inheritdoc />
    public partial class PhysicalShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhysicalShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OpenTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalShops", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysicalShops");
        }
    }
}
