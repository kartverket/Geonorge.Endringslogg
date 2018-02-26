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
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Application = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ElementId = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

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
                name: "IX_LogEntries_User",
                table: "LogEntries",
                column: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");
        }
    }
}
