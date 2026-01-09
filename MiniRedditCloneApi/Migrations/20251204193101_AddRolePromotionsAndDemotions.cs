using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRolePromotionsAndDemotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role_demotions",
                columns: table => new
                {
                    nerd_id = table.Column<string>(type: "text", nullable: false),
                    herd_id = table.Column<int>(type: "integer", nullable: false),
                    moderator_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_demotions", x => new { x.nerd_id, x.herd_id, x.moderator_id });
                    table.ForeignKey(
                        name: "FK_role_demotions_herd_nerds_nerd_id_herd_id",
                        columns: x => new { x.nerd_id, x.herd_id },
                        principalTable: "herd_nerds",
                        principalColumns: new[] { "nerd_id", "herd_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_demotions_nerds_moderator_id",
                        column: x => x.moderator_id,
                        principalTable: "nerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_promotions",
                columns: table => new
                {
                    nerd_id = table.Column<string>(type: "text", nullable: false),
                    herd_id = table.Column<int>(type: "integer", nullable: false),
                    moderator_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_promotions", x => new { x.nerd_id, x.herd_id, x.moderator_id });
                    table.ForeignKey(
                        name: "FK_role_promotions_herd_nerds_nerd_id_herd_id",
                        columns: x => new { x.nerd_id, x.herd_id },
                        principalTable: "herd_nerds",
                        principalColumns: new[] { "nerd_id", "herd_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_promotions_nerds_moderator_id",
                        column: x => x.moderator_id,
                        principalTable: "nerds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_demotions_moderator_id",
                table: "role_demotions",
                column: "moderator_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_promotions_moderator_id",
                table: "role_promotions",
                column: "moderator_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_demotions");

            migrationBuilder.DropTable(
                name: "role_promotions");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:09 PM
# Update Fri, Jan  9, 2026  9:25:49 PM
# Update Fri, Jan  9, 2026  9:34:33 PM
