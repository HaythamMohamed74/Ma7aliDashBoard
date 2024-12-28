using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ma7ali.DashBoard.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class addColorSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvailableColor",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvailableSize",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableColor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvailableSize",
                table: "Products");
        }
    }
}
