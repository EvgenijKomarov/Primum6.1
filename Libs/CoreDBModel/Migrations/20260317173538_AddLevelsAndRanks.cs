using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelsAndRanks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGrading_Lessons_LessonId",
                table: "StudentGrading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGrading",
                table: "StudentGrading");

            migrationBuilder.DropColumn(
                name: "EarningMultiplier",
                table: "TeacherProfiles");

            migrationBuilder.RenameTable(
                name: "StudentGrading",
                newName: "StudentGradings");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGrading_LessonId",
                table: "StudentGradings",
                newName: "IX_StudentGradings_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGrading_Id",
                table: "StudentGradings",
                newName: "IX_StudentGradings_Id");

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "TeacherProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "TeacherProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "StudentProfiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Abonements",
                type: "real",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGradings",
                table: "StudentGradings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredExperience = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredExperience = table.Column<int>(type: "int", nullable: false),
                    CoinDiscount = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRanks", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "RankId",
                keyValue: 0,
                column: "RankId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "StudentProfiles",
                keyColumn: "RankId",
                keyValue: 0,
                column: "RankId",
                value: -1);

            migrationBuilder.UpdateData(
                table: "TeacherProfiles",
                keyColumn: "RankId",
                keyValue: 0,
                column: "RankId",
                value: -1);

            migrationBuilder.CreateTable(
                name: "TeacherRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredExperience = table.Column<int>(type: "int", nullable: false),
                    EarningMultiplier = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherRanks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CourseRanks",
                columns: new[] { "Id", "CreatedAt", "Level", "Rank", "RequiredExperience" },
                values: new object[] { -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Новый", 0 });

            migrationBuilder.InsertData(
                table: "StudentRanks",
                columns: new[] { "Id", "CoinDiscount", "CreatedAt", "Level", "Rank", "RequiredExperience" },
                values: new object[] { -1, 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Новенький", 0 });

            migrationBuilder.InsertData(
                table: "TeacherRanks",
                columns: new[] { "Id", "CreatedAt", "EarningMultiplier", "Level", "Rank", "RequiredExperience" },
                values: new object[] { -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.3f, 1, "Начинающий наставник", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_RankId",
                table: "TeacherProfiles",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_RankId",
                table: "StudentProfiles",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RankId",
                table: "Courses",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRanks_Id",
                table: "CourseRanks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentRanks_Id",
                table: "StudentRanks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherRanks_Id",
                table: "TeacherRanks",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseRanks_RankId",
                table: "Courses",
                column: "RankId",
                principalTable: "CourseRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGradings_Lessons_LessonId",
                table: "StudentGradings",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_StudentRanks_RankId",
                table: "StudentProfiles",
                column: "RankId",
                principalTable: "StudentRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherProfiles_TeacherRanks_RankId",
                table: "TeacherProfiles",
                column: "RankId",
                principalTable: "TeacherRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseRanks_RankId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGradings_Lessons_LessonId",
                table: "StudentGradings");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_StudentRanks_RankId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherProfiles_TeacherRanks_RankId",
                table: "TeacherProfiles");

            migrationBuilder.DropTable(
                name: "CourseRanks");

            migrationBuilder.DropTable(
                name: "StudentRanks");

            migrationBuilder.DropTable(
                name: "TeacherRanks");

            migrationBuilder.DropIndex(
                name: "IX_TeacherProfiles_RankId",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_RankId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Courses_RankId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGradings",
                table: "StudentGradings");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Abonements");

            migrationBuilder.RenameTable(
                name: "StudentGradings",
                newName: "StudentGrading");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGradings_LessonId",
                table: "StudentGrading",
                newName: "IX_StudentGrading_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGradings_Id",
                table: "StudentGrading",
                newName: "IX_StudentGrading_Id");

            migrationBuilder.AddColumn<float>(
                name: "EarningMultiplier",
                table: "TeacherProfiles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGrading",
                table: "StudentGrading",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGrading_Lessons_LessonId",
                table: "StudentGrading",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
