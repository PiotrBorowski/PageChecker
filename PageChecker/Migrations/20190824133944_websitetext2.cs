using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class websitetext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "WebsiteTexts",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "WebsiteTexts",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}
