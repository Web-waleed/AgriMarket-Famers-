using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class hnbvcxz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_farmers_FarmerId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_FarmerId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImg",
                table: "farmers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "farmers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "farmers");

            migrationBuilder.DropColumn(
                name: "ProductImg",
                table: "farmers");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "farmers");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "farmers");

            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_FarmerId",
                table: "products",
                column: "FarmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_farmers_FarmerId",
                table: "products",
                column: "FarmerId",
                principalTable: "farmers",
                principalColumn: "FarmerId");
        }
    }
}
