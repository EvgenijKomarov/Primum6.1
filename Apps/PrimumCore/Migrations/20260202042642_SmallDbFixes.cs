using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimumCore.Migrations
{
    /// <inheritdoc />
    public partial class SmallDbFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                table: "AbonementShedules");

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
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                table: "AbonementShedules",
                column: "TeacherSheduleId",
                principalTable: "TeacherShedules",
                principalColumn: "TeacherSheduleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId1",
                table: "AbonementShedules",
                column: "TeacherSheduleId1",
                principalTable: "TeacherShedules",
                principalColumn: "TeacherSheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                table: "AbonementShedules");

            migrationBuilder.DropForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.DropIndex(
                name: "IX_AbonementShedules_TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.DropColumn(
                name: "TeacherSheduleId1",
                table: "AbonementShedules");

            migrationBuilder.AddForeignKey(
                name: "FK_AbonementShedules_TeacherShedules_TeacherSheduleId",
                table: "AbonementShedules",
                column: "TeacherSheduleId",
                principalTable: "TeacherShedules",
                principalColumn: "TeacherSheduleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
