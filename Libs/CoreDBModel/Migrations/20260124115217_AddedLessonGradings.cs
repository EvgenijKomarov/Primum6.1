using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedLessonGradings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "IncendentLogs");

            migrationBuilder.RenameColumn(
                name: "ObjectInfo",
                table: "IncendentLogs",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DecisionDate",
                table: "IncendentLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StudentGrading",
                columns: table => new
                {
                    StudentGradingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    HomeworkGrade = table.Column<int>(type: "int", nullable: false),
                    LessonActivityGrade = table.Column<int>(type: "int", nullable: false),
                    RepetitionOfMaterialGrade = table.Column<int>(type: "int", nullable: false),
                    StudyInitiativeGrade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGrading", x => x.StudentGradingId);
                    table.ForeignKey(
                        name: "FK_StudentGrading_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonId",
                table: "Lessons",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrading_LessonId",
                table: "StudentGrading",
                column: "LessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrading_StudentGradingId",
                table: "StudentGrading",
                column: "StudentGradingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentGrading");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "DecisionDate",
                table: "IncendentLogs");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "IncendentLogs",
                newName: "ObjectInfo");

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "IncendentLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
