using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class websiteText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "BodyDifference",
                table: "Pages");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryTextId",
                table: "Pages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SecondaryTextId",
                table: "Pages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WebsiteTexts",
                columns: table => new
                {
                    WebsiteTextId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteTexts", x => x.WebsiteTextId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PrimaryTextId",
                table: "Pages",
                column: "PrimaryTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_SecondaryTextId",
                table: "Pages",
                column: "SecondaryTextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_WebsiteTexts_PrimaryTextId",
                table: "Pages",
                column: "PrimaryTextId",
                principalTable: "WebsiteTexts",
                principalColumn: "WebsiteTextId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_WebsiteTexts_SecondaryTextId",
                table: "Pages",
                column: "SecondaryTextId",
                principalTable: "WebsiteTexts",
                principalColumn: "WebsiteTextId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_WebsiteTexts_PrimaryTextId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_WebsiteTexts_SecondaryTextId",
                table: "Pages");

            migrationBuilder.DropTable(
                name: "WebsiteTexts");

            migrationBuilder.DropIndex(
                name: "IX_Pages_PrimaryTextId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_SecondaryTextId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PrimaryTextId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "SecondaryTextId",
                table: "Pages");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Pages",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyDifference",
                table: "Pages",
                type: "ntext",
                nullable: true);
        }
    }
}
