using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeTestDemo.Infrastructure.Migrations
{
    public partial class MyfirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameTitle = table.Column<string>(nullable: true),
                    TeamA = table.Column<string>(maxLength: 50, nullable: true),
                    TeamB = table.Column<string>(maxLength: 50, nullable: true),
                    TeamAScore = table.Column<int>(nullable: false),
                    TeamBScore = table.Column<int>(nullable: false),
                    Employee = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
