using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scula.Migrations
{
    /// <inheritdoc />
    public partial class belea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Assignments_AssignmentId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "SubjectModelTestModel");

            migrationBuilder.DropTable(
                name: "TestModel");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_AssignmentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Assignments",
                newName: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Assignments",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentId",
                table: "Subjects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TestModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectModelTestModel",
                columns: table => new
                {
                    SubjectsId = table.Column<string>(type: "text", nullable: false),
                    TestsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectModelTestModel", x => new { x.SubjectsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_SubjectModelTestModel_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectModelTestModel_TestModel_TestsId",
                        column: x => x.TestsId,
                        principalTable: "TestModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_AssignmentId",
                table: "Subjects",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectModelTestModel_TestsId",
                table: "SubjectModelTestModel",
                column: "TestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Assignments_AssignmentId",
                table: "Subjects",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
