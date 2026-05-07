using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class MoneyPolicyRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Users");

            migrationBuilder.AddColumn<decimal>(
                name: "Cash",
                table: "StudentProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "StudentProfiles");

            migrationBuilder.AddColumn<long>(
                name: "Cash",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
