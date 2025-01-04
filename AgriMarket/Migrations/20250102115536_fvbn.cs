using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriMarket.Migrations
{
    /// <inheritdoc />
    public partial class fvbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "contactIUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "contactIUs");
        }
    }
}
