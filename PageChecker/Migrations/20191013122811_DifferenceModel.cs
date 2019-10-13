using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class DifferenceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_WebsiteTexts_SecondaryTextId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_SecondaryTextId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "SecondaryTextId",
                table: "Pages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryTextId",
                table: "Pages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SecondaryTextId",
                table: "Pages",
                column: "SecondaryTextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_WebsiteTexts_SecondaryTextId",
                table: "Pages",
                column: "SecondaryTextId",
                principalTable: "WebsiteTexts",
                principalColumn: "WebsiteTextId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
