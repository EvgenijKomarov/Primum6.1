using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimumCore.Migrations
{
    /// <inheritdoc />
    public partial class Incident_Renaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncendentLogs");

            migrationBuilder.CreateTable(
                name: "IncidentLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminProfileId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevisioned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_IncidentLogs_AdminProfiles_AdminProfileId",
                        column: x => x.AdminProfileId,
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncidentLogs_AdminProfileId",
                table: "IncidentLogs",
                column: "AdminProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentLogs_LogId",
                table: "IncidentLogs",
                column: "LogId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentLogs");

            migrationBuilder.CreateTable(
                name: "IncendentLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminProfileId = table.Column<int>(type: "int", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
    }
}
