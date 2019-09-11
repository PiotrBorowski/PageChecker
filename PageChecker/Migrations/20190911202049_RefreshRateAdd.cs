using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class RefreshRateAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshRate2",
                table: "Pages");

            migrationBuilder.AddColumn<int>(
                name: "RefreshRate",
                table: "Pages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshRate",
                table: "Pages");

            migrationBuilder.AddColumn<int>(
                name: "RefreshRate2",
                table: "Pages",
                nullable: false,
                defaultValue: 0);
        }
    }
}
