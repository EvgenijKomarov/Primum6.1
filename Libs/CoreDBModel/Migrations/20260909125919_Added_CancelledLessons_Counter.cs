using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class Added_CancelledLessons_Counter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CancelledLessons",
                table: "Abonements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledLessons",
                table: "Abonements");
        }
    }
}
