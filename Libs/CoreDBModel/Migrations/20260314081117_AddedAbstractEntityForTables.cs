using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedAbstractEntityForTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenId",
                table: "VerificationTokens",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationTokens_TokenId",
                table: "VerificationTokens",
                newName: "IX_VerificationTokens_Id");

            migrationBuilder.RenameColumn(
                name: "TeacherSheduleId",
                table: "TeacherShedules",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherShedules_TeacherSheduleId",
                table: "TeacherShedules",
                newName: "IX_TeacherShedules_Id");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TeacherProfiles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherProfiles_TeacherId",
                table: "TeacherProfiles",
                newName: "IX_TeacherProfiles_Id");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentProfiles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProfiles_StudentId",
                table: "StudentProfiles",
                newName: "IX_StudentProfiles_Id");

            migrationBuilder.RenameColumn(
                name: "StudentGradingId",
                table: "StudentGrading",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGrading_StudentGradingId",
                table: "StudentGrading",
                newName: "IX_StudentGrading_Id");

            migrationBuilder.RenameColumn(
                name: "PromocodeId",
                table: "Promocodes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Promocodes_PromocodeId",
                table: "Promocodes",
                newName: "IX_Promocodes_Id");

            migrationBuilder.RenameColumn(
                name: "AdminPermissionId",
                table: "Permissions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_AdminPermissionId",
                table: "Permissions",
                newName: "IX_Permissions_Id");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_LessonId",
                table: "Lessons",
                newName: "IX_Lessons_Id");

            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "IncidentLogs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_IncidentLogs_LogId",
                table: "IncidentLogs",
                newName: "IX_IncidentLogs_Id");

            migrationBuilder.RenameColumn(
                name: "CourseThemeId",
                table: "CourseThemes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CourseThemes_CourseThemeId",
                table: "CourseThemes",
                newName: "IX_CourseThemes_Id");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseId",
                table: "Courses",
                newName: "IX_Courses_Id");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "AdminProfiles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AdminProfiles_AdminId",
                table: "AdminProfiles",
                newName: "IX_AdminProfiles_Id");

            migrationBuilder.RenameColumn(
                name: "AbonementSheduleId",
                table: "AbonementShedules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AbonementId",
                table: "Abonements",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Abonements_AbonementId",
                table: "Abonements",
                newName: "IX_Abonements_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VerificationTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeacherShedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeacherShedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeacherProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeacherProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentGrading",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "StudentGrading",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Promocodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Promocodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "IncidentLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "IncidentLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseThemes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CourseThemes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AdminProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AdminProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AbonementShedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AbonementShedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Abonements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abonements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VerificationTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VerificationTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeacherShedules");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeacherShedules");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentGrading");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "StudentGrading");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "IncidentLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "IncidentLogs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseThemes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CourseThemes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AdminProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AdminProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AbonementShedules");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AbonementShedules");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Abonements");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Abonements");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VerificationTokens",
                newName: "TokenId");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationTokens_Id",
                table: "VerificationTokens",
                newName: "IX_VerificationTokens_TokenId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherShedules",
                newName: "TeacherSheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherShedules_Id",
                table: "TeacherShedules",
                newName: "IX_TeacherShedules_TeacherSheduleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherProfiles",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherProfiles_Id",
                table: "TeacherProfiles",
                newName: "IX_TeacherProfiles_TeacherId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudentProfiles",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProfiles_Id",
                table: "StudentProfiles",
                newName: "IX_StudentProfiles_StudentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StudentGrading",
                newName: "StudentGradingId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGrading_Id",
                table: "StudentGrading",
                newName: "IX_StudentGrading_StudentGradingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Promocodes",
                newName: "PromocodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Promocodes_Id",
                table: "Promocodes",
                newName: "IX_Promocodes_PromocodeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Permissions",
                newName: "AdminPermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_Id",
                table: "Permissions",
                newName: "IX_Permissions_AdminPermissionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lessons",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_Id",
                table: "Lessons",
                newName: "IX_Lessons_LessonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IncidentLogs",
                newName: "LogId");

            migrationBuilder.RenameIndex(
                name: "IX_IncidentLogs_Id",
                table: "IncidentLogs",
                newName: "IX_IncidentLogs_LogId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CourseThemes",
                newName: "CourseThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseThemes_Id",
                table: "CourseThemes",
                newName: "IX_CourseThemes_CourseThemeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_Id",
                table: "Courses",
                newName: "IX_Courses_CourseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AdminProfiles",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminProfiles_Id",
                table: "AdminProfiles",
                newName: "IX_AdminProfiles_AdminId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AbonementShedules",
                newName: "AbonementSheduleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Abonements",
                newName: "AbonementId");

            migrationBuilder.RenameIndex(
                name: "IX_Abonements_Id",
                table: "Abonements",
                newName: "IX_Abonements_AbonementId");
        }
    }
}
