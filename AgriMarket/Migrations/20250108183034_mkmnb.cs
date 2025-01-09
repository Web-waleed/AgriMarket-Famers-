using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class mkmnb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_farmers_FarmerId",
                table: "products");

            migrationBuilder.AlterColumn<int>(
                name: "FarmerId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "FarmerId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
