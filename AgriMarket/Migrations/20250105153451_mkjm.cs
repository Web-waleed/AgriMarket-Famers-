using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class mkjm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductURL",
                table: "BestSeller");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "BestSeller",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "BestSeller");

            migrationBuilder.AddColumn<string>(
                name: "ProductURL",
                table: "BestSeller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
