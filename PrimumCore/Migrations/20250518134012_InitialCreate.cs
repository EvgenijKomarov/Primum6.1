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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    Patronymic = table.Column<string>(type: "TEXT", nullable: true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: true),
                    AdminId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminProfiles",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Permissions = table.Column<byte[]>(type: "BLOB", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_Users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApproveStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherProfiles",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    About = table.Column<string>(type: "TEXT", nullable: true),
                    ApproveStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherProfiles", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_TeacherProfiles_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxLessons = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    FreeLessons = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    ApproveStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_TeacherProfiles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherProfiles",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherShedules",
                columns: table => new
                {
                    TeacherSheduleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayOfWeek = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<int>(type: "INTEGER", nullable: false)
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
                    AbonementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerLesson = table.Column<int>(type: "INTEGER", nullable: false),
                    PaidLessons = table.Column<int>(type: "INTEGER", nullable: false),
                    AbonementStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonements", x => x.AbonementId);
                    table.ForeignKey(
                        name: "FK_Abonements_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Abonements_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbonementShedules",
                columns: table => new
                {
                    AbonementSheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherSheduleId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastIteration = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonementShedules", x => x.AbonementSheduleId);
                    table.ForeignKey(
                        name: "FK_AbonementShedules_Abonements_AbonementSheduleId",
                        column: x => x.AbonementSheduleId,
                        principalTable: "Abonements",
                        principalColumn: "AbonementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                        column: x => x.TeacherSheduleId,
                        principalTable: "TeacherShedules",
                        principalColumn: "TeacherSheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbonementId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_Abonements_AbonementId",
                        column: x => x.AbonementId,
                        principalTable: "Abonements",
                        principalColumn: "AbonementId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Courses_CourseId",
                table: "Courses",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

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
                name: "IX_TeacherProfiles_TeacherId",
                table: "TeacherProfiles",
                column: "TeacherId",
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
                name: "TeacherProfiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
