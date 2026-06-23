using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class added_convertion_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ConvertionIndex",
                table: "TeacherProfiles",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertionIndex",
                table: "TeacherProfiles");
        }
    }
}
