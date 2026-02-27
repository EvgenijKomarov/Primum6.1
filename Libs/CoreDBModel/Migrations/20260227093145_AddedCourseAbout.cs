using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedCourseAbout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.DropIndex(
                name: "IX_AbonementShedules_TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.DropColumn(
                name: "TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "TeacherSheduleId1",
                table: "AbonementShedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbonementShedules_TeacherSheduleId1",
                table: "AbonementShedules",
                column: "TeacherSheduleId1",
                unique: true,
                filter: "[TeacherSheduleId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId1",
                table: "AbonementShedules",
                column: "TeacherSheduleId1",
                principalTable: "TeacherShedules",
                principalColumn: "TeacherSheduleId");
        }
    }
}
