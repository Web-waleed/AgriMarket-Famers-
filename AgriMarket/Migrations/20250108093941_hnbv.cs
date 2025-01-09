using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class hnbv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "farmers",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_farmers", x => x.FarmerId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_farmers_FarmerId",
                table: "products");

            migrationBuilder.DropTable(
                name: "farmers");

            migrationBuilder.DropIndex(
                name: "IX_products_FarmerId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "products");
        }
    }
}
