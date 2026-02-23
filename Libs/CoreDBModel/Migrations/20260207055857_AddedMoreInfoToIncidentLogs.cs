using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreInfoToIncidentLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Meaning",
                table: "IncidentLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObjectId",
                table: "IncidentLogs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meaning",
                table: "IncidentLogs");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "IncidentLogs");
        }
    }
}
