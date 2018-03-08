using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Geonorge.Endringslogg.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiKey = table.Column<string>(nullable: true),
                    ApplicationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Application = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ElementId = table.Column<string>(nullable: true),
                    Operation = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApiKey",
                table: "Applications",
                column: "ApiKey",
                unique: true,
                filter: "[ApiKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_Application",
                table: "LogEntries",
                column: "Application");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_DateTime",
                table: "LogEntries",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_ElementId",
                table: "LogEntries",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_Operation",
                table: "LogEntries",
                column: "Operation");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_User",
                table: "LogEntries",
                column: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "LogEntries");
        }
    }
}
