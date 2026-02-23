using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreDBModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminPermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "AdminProfiles");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    AdminPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminProfileId = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    PromoterAdminProfileId = table.Column<int>(type: "int", nullable: true),
                    PromotionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.AdminPermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_AdminProfiles_AdminProfileId",
                        column: x => x.AdminProfileId,
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_AdminProfiles_PromoterAdminProfileId",
                        column: x => x.PromoterAdminProfileId,
                        principalTable: "AdminProfiles",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AdminPermissionId",
                table: "Permissions",
                column: "AdminPermissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AdminProfileId",
                table: "Permissions",
                column: "AdminProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PromoterAdminProfileId",
                table: "Permissions",
                column: "PromoterAdminProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "AdminProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
