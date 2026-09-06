using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class Added_TeacherEarning_to_Lesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TeacherEarning",
                table: "Lessons",
                type: "numeric",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -1,
                column: "EarningMultiplier",
                value: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherEarning",
                table: "Lessons");

            migrationBuilder.UpdateData(
                table: "TeacherRanks",
                keyColumn: "Id",
                keyValue: -1,
                column: "EarningMultiplier",
                value: 0.3f);
        }
    }
}
