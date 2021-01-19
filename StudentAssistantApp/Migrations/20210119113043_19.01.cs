using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentAssistantApp.Migrations
{
    public partial class _1901 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBEvents",
                columns: table => new
                {
                    DBEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEvent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBEvents", x => x.DBEventId);
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
                name: "DBSubjects",
                columns: table => new
                {
                    DBSubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBSubjects", x => x.DBSubjectId);
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

            migrationBuilder.CreateTable(
                name: "DBMarks",
                columns: table => new
                {
                    DBMarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DBSubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMarks", x => x.DBMarkId);
                    table.ForeignKey(
                        name: "FK_DBMarks_DBSubjects_DBSubjectId",
                        column: x => x.DBSubjectId,
                        principalTable: "DBSubjects",
                        principalColumn: "DBSubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBMarks_DBSubjectId",
                table: "DBMarks",
                column: "DBSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBEvents");

            migrationBuilder.DropTable(
                name: "DBMarks");

            migrationBuilder.DropTable(
                name: "DBNotes");

            migrationBuilder.DropTable(
                name: "DBTasks");

            migrationBuilder.DropTable(
                name: "DBUsers");

            migrationBuilder.DropTable(
                name: "DBSubjects");
        }
    }
}
