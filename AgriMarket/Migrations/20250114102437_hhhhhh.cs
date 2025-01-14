using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class hhhhhh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "sliders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sliders_ProductId",
                table: "sliders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_sliders_Products_ProductId",
                table: "sliders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sliders_Products_ProductId",
                table: "sliders");

            migrationBuilder.DropIndex(
                name: "IX_sliders_ProductId",
                table: "sliders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "sliders");
        }
    }
}
