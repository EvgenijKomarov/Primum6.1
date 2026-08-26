using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTeacherPaymentCredits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "BankBIC",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "INN",
                table: "TeacherProfiles");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TeacherProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "TeacherProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankBIC",
                table: "TeacherProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "TeacherProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TeacherProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
