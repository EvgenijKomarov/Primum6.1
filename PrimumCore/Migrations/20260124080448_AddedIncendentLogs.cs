using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimumCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedIncendentLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncendentLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminProfileId = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    ObjectInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRevisioned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncendentLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_IncendentLogs_AdminProfiles_AdminProfileId",
                        column: x => x.AdminProfileId,
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncendentLogs_AdminProfileId",
                table: "IncendentLogs",
                column: "AdminProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_IncendentLogs_LogId",
                table: "IncendentLogs",
                column: "LogId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncendentLogs");
        }
    }
}
