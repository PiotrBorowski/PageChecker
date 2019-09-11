using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PageCheckerAPI.Migrations
{
    public partial class InsertZeroGuidText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WebsiteTexts",
                columns: new[] {"WebsiteTextId", "Text"},
                values: new object[] {Guid.Empty, String.Empty}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
