using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class bgb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "BestSeller");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "BestSeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "BestSeller");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "BestSeller",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
