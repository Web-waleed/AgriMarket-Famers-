using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class bvcd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FarmerEmail",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FarmerName",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FarmerNumber",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmerEmail",
                table: "products");

            migrationBuilder.DropColumn(
                name: "FarmerName",
                table: "products");

            migrationBuilder.DropColumn(
                name: "FarmerNumber",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "farmers",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                principalColumn: "FarmerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
