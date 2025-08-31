using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimumCore.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminProfiles_Users_AdminId",
                table: "AdminProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Users_StudentId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherProfiles_Users_TeacherId",
                table: "TeacherProfiles");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TeacherProfiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StudentProfiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdminProfiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherProfiles_UserId",
                table: "TeacherProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_UserId",
                table: "AdminProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminProfiles_Users_UserId",
                table: "AdminProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Users_UserId",
                table: "StudentProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherProfiles_Users_UserId",
                table: "TeacherProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminProfiles_Users_UserId",
                table: "AdminProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Users_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherProfiles_Users_UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_TeacherProfiles_UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_AdminProfiles_UserId",
                table: "AdminProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdminProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminProfiles_Users_AdminId",
                table: "AdminProfiles",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Users_StudentId",
                table: "StudentProfiles",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherProfiles_Users_TeacherId",
                table: "TeacherProfiles",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
