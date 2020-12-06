using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentAssistantApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBMarks",
                columns: table => new
                {
                    DBMarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMarks", x => x.DBMarkId);
                });

            migrationBuilder.CreateTable(
                name: "DBNotes",
                columns: table => new
                {
                    DBNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteHeadline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBNotes", x => x.DBNoteId);
                });

            migrationBuilder.CreateTable(
                name: "DBTasks",
                columns: table => new
                {
                    DBTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskHeadline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBTasks", x => x.DBTaskId);
                });

            migrationBuilder.CreateTable(
                name: "DBUsers",
                columns: table => new
                {
                    DBUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTimeLogged = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUsers", x => x.DBUserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBMarks");

            migrationBuilder.DropTable(
                name: "DBNotes");

            migrationBuilder.DropTable(
                name: "DBTasks");

            migrationBuilder.DropTable(
                name: "DBUsers");
        }
    }
}
