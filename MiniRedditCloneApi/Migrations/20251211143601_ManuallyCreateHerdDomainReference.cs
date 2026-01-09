using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class ManuallyCreateHerdDomainReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainHerd");

            migrationBuilder.CreateTable(
                name: "herd_domains",
                columns: table => new
                {
                    herd_id = table.Column<int>(type: "integer", nullable: false),
                    domain_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_herd_domains", x => new { x.herd_id, x.domain_id });
                    table.ForeignKey(
                        name: "FK_herd_domains_domains_domain_id",
                        column: x => x.domain_id,
                        principalTable: "domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_herd_domains_herds_herd_id",
                        column: x => x.herd_id,
                        principalTable: "herds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insight_topics",
                columns: table => new
                {
                    insight_id = table.Column<int>(type: "integer", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insight_topics", x => new { x.insight_id, x.topic_id });
                    table.ForeignKey(
                        name: "FK_insight_topics_insights_insight_id",
                        column: x => x.insight_id,
                        principalTable: "insights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_insight_topics_topics_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_herd_domains_domain_id",
                table: "herd_domains",
                column: "domain_id");

            migrationBuilder.CreateIndex(
                name: "IX_insight_topics_topic_id",
                table: "insight_topics",
                column: "topic_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "herd_domains");

            migrationBuilder.DropTable(
                name: "insight_topics");

            migrationBuilder.CreateTable(
                name: "DomainHerd",
                columns: table => new
                {
                    DomainsId = table.Column<int>(type: "integer", nullable: false),
                    HerdsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainHerd", x => new { x.DomainsId, x.HerdsId });
                    table.ForeignKey(
                        name: "FK_DomainHerd_domains_DomainsId",
                        column: x => x.DomainsId,
                        principalTable: "domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DomainHerd_herds_HerdsId",
                        column: x => x.HerdsId,
                        principalTable: "herds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainHerd_HerdsId",
                table: "DomainHerd",
                column: "HerdsId");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:10 PM
# Update Fri, Jan  9, 2026  9:25:49 PM
# Update Fri, Jan  9, 2026  9:34:33 PM
