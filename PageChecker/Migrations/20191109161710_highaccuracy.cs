using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class highaccuracy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HighAccuracy",
                table: "Pages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighAccuracy",
                table: "Pages");
        }
    }
}
