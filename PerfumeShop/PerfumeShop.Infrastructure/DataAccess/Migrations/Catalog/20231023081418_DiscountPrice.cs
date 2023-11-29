using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Infrastructure.DataAccess.Migrations.Catalog
{
    /// <inheritdoc />
    public partial class DiscountPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AromaTypes_AromaTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ReleaseForms_ReleaseFormId",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AromaTypes_AromaTypeId",
                table: "Products",
                column: "AromaTypeId",
                principalTable: "AromaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ReleaseForms_ReleaseFormId",
                table: "Products",
                column: "ReleaseFormId",
                principalTable: "ReleaseForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AromaTypes_AromaTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ReleaseForms_ReleaseFormId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AromaTypes_AromaTypeId",
                table: "Products",
                column: "AromaTypeId",
                principalTable: "AromaTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ReleaseForms_ReleaseFormId",
                table: "Products",
                column: "ReleaseFormId",
                principalTable: "ReleaseForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
