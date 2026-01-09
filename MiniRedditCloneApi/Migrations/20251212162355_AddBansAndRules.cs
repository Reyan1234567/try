using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBansAndRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rules",
                table: "herds",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bans",
                columns: table => new
                {
                    nerd_id = table.Column<string>(type: "text", nullable: false),
                    herd_id = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    banned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bans", x => new { x.nerd_id, x.herd_id });
                    table.ForeignKey(
                        name: "FK_bans_herds_herd_id",
                        column: x => x.herd_id,
                        principalTable: "herds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bans_nerds_nerd_id",
                        column: x => x.nerd_id,
                        principalTable: "nerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bans_herd_id",
                table: "bans",
                column: "herd_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bans");

            migrationBuilder.DropColumn(
                name: "rules",
                table: "herds");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:10 PM
# Update Fri, Jan  9, 2026  9:25:50 PM
# Update Fri, Jan  9, 2026  9:34:34 PM
