using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimumCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseThemes",
                columns: table => new
                {
                    CourseThemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseThemes", x => x.CourseThemeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cash = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminProfiles",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherProfiles",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EarningMultiplier = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherProfiles", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_TeacherProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    MaxLessons = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FreeLessons = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CourseThemeId = table.Column<int>(type: "int", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_CourseThemes_CourseThemeId",
                        column: x => x.CourseThemeId,
                        principalTable: "CourseThemes",
                        principalColumn: "CourseThemeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_TeacherProfiles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherProfiles",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherShedules",
                columns: table => new
                {
                    TeacherSheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherShedules", x => x.TeacherSheduleId);
                    table.ForeignKey(
                        name: "FK_TeacherShedules_TeacherProfiles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherProfiles",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abonements",
                columns: table => new
                {
                    AbonementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PricePerLesson = table.Column<int>(type: "int", nullable: false),
                    AbonementStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonements", x => x.AbonementId);
                    table.ForeignKey(
                        name: "FK_Abonements_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Abonements_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbonementShedules",
                columns: table => new
                {
                    AbonementSheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbonementId = table.Column<int>(type: "int", nullable: false),
                    TeacherSheduleId = table.Column<int>(type: "int", nullable: false),
                    LastIteration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonementShedules", x => x.AbonementSheduleId);
                    table.ForeignKey(
                        name: "FK_AbonementShedules_Abonements_AbonementId",
                        column: x => x.AbonementId,
                        principalTable: "Abonements",
                        principalColumn: "AbonementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                        column: x => x.TeacherSheduleId,
                        principalTable: "TeacherShedules",
                        principalColumn: "TeacherSheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbonementId = table.Column<int>(type: "int", nullable: false),
                    StudentLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_Abonements_AbonementId",
                        column: x => x.AbonementId,
                        principalTable: "Abonements",
                        principalColumn: "AbonementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonements_AbonementId",
                table: "Abonements",
                column: "AbonementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abonements_CourseId",
                table: "Abonements",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Abonements_StudentId",
                table: "Abonements",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbonementShedules_AbonementId",
                table: "AbonementShedules",
                column: "AbonementId");

            migrationBuilder.CreateIndex(
                name: "IX_AbonementShedules_TeacherSheduleId",
                table: "AbonementShedules",
                column: "TeacherSheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_AdminId",
                table: "AdminProfiles",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_UserId",
                table: "AdminProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseId",
                table: "Courses",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseThemeId",
                table: "Courses",
                column: "CourseThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseThemes_CourseThemeId",
                table: "CourseThemes",
                column: "CourseThemeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_AbonementId",
                table: "Lessons",
                column: "AbonementId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_StudentId",
                table: "StudentProfiles",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_TeacherId",
                table: "TeacherProfiles",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_UserId",
                table: "TeacherProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherShedules_TeacherId",
                table: "TeacherShedules",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherShedules_TeacherSheduleId",
                table: "TeacherShedules",
                column: "TeacherSheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbonementShedules");

            migrationBuilder.DropTable(
                name: "AdminProfiles");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "TeacherShedules");

            migrationBuilder.DropTable(
                name: "Abonements");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "CourseThemes");

            migrationBuilder.DropTable(
                name: "TeacherProfiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
