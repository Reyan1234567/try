using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainsAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "domains",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_domains", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "topics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    DomainId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topics", x => x.id);
                    table.ForeignKey(
                        name: "FK_topics_domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsightTopic",
                columns: table => new
                {
                    InsightsId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsightTopic", x => new { x.InsightsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_InsightTopic_insights_InsightsId",
                        column: x => x.InsightsId,
                        principalTable: "insights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsightTopic_topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "topics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "domains",
                columns: new[] { "id", "is_system", "name" },
                values: new object[,]
                {
                    { 1, true, "Science" },
                    { 2, true, "Technology & Computing" },
                    { 3, true, "Mathematics" },
                    { 4, true, "Arts & Literature" },
                    { 5, true, "Culture & Humanities" },
                    { 6, true, "Gaming & Entertainment" },
                    { 7, true, "Space & Astronomy" },
                    { 8, true, "Engineering & Making" }
                });

            migrationBuilder.InsertData(
                table: "topics",
                columns: new[] { "id", "DomainId", "name" },
                values: new object[,]
                {
                    { 1, 1, "Physics" },
                    { 2, 1, "Chemistry" },
                    { 3, 1, "Biology" },
                    { 4, 1, "Earth Science" },
                    { 5, 1, "Environmental Science" },
                    { 6, 1, "Neuroscience" },
                    { 7, 1, "Psychology" },
                    { 8, 1, "Anthropology" },
                    { 9, 1, "Ecology" },
                    { 10, 1, "Zoology" },
                    { 11, 1, "Genetics" },
                    { 12, 1, "Medicine" },
                    { 13, 1, "General" },
                    { 14, 2, "Programming" },
                    { 15, 2, "Algorithms" },
                    { 16, 2, "Data Structures" },
                    { 17, 2, "Web Development" },
                    { 18, 2, "Mobile Development" },
                    { 19, 5, "Game Development" },
                    { 20, 2, "Cybersecurity" },
                    { 21, 2, "Databases" },
                    { 22, 2, "DevOps" },
                    { 23, 2, "AI & Machine Learning" },
                    { 24, 2, "Cloud Computing" },
                    { 25, 2, "System Design" },
                    { 26, 2, "Networking" },
                    { 27, 2, "General" },
                    { 28, 3, "Algebra" },
                    { 29, 3, "Calculus" },
                    { 30, 3, "Geometry" },
                    { 31, 3, "Statistics" },
                    { 32, 3, "Probability" },
                    { 33, 3, "Number Theory" },
                    { 34, 3, "Linear Algebra" },
                    { 35, 3, "Discrete Math" },
                    { 36, 3, "Mathematical Logic" },
                    { 37, 3, "Topology" },
                    { 38, 3, "General" },
                    { 39, 4, "Fiction" },
                    { 40, 4, "Non-Fiction" },
                    { 41, 4, "Poetry" },
                    { 42, 4, "Visual Arts" },
                    { 43, 4, "Music" },
                    { 44, 4, "Film Analysis" },
                    { 45, 4, "Photography" },
                    { 46, 4, "Creative Writing" },
                    { 47, 4, "Philosophy" },
                    { 48, 4, "History of Art" },
                    { 49, 4, "General" },
                    { 50, 5, "Linguistics" },
                    { 51, 5, "History" },
                    { 52, 5, "Sociology" },
                    { 53, 5, "World Cultures" },
                    { 54, 5, "Religion" },
                    { 55, 5, "Mythology" },
                    { 56, 5, "Politics" },
                    { 57, 5, "Ethics" },
                    { 58, 5, "Economics" },
                    { 59, 5, "General" },
                    { 60, 6, "Video Games" },
                    { 61, 6, "Game Development" },
                    { 62, 6, "Board Games" },
                    { 63, 6, "Anime" },
                    { 64, 6, "Comics" },
                    { 65, 6, "Movies" },
                    { 66, 6, "TV Shows" },
                    { 67, 6, "Esports" },
                    { 68, 6, "Tabletop RPGs" },
                    { 69, 6, "Digital RPGs" },
                    { 70, 6, "General" },
                    { 71, 7, "Astrophysics" },
                    { 72, 7, "Cosmology" },
                    { 73, 7, "Space Exploration" },
                    { 74, 7, "Telescopes" },
                    { 75, 7, "Planetary Science" },
                    { 76, 7, "Black Holes" },
                    { 77, 7, "Astrobiology" },
                    { 78, 7, "Star Formation" },
                    { 79, 7, "General" },
                    { 80, 8, "Electronics" },
                    { 81, 8, "Robotics" },
                    { 82, 8, "Mechanical Engineering" },
                    { 83, 8, "Electrical Engineering" },
                    { 84, 8, "Civil Engineering" },
                    { 85, 8, "3D Printing" },
                    { 86, 8, "DIY Projects" },
                    { 87, 8, "Hardware Hacking" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainHerd_HerdsId",
                table: "DomainHerd",
                column: "HerdsId");

            migrationBuilder.CreateIndex(
                name: "IX_domains_name",
                table: "domains",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsightTopic_TopicsId",
                table: "InsightTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_topics_DomainId_name",
                table: "topics",
                columns: new[] { "DomainId", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainHerd");

            migrationBuilder.DropTable(
                name: "InsightTopic");

            migrationBuilder.DropTable(
                name: "topics");

            migrationBuilder.DropTable(
                name: "domains");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:07 PM
# Update Fri, Jan  9, 2026  9:25:47 PM
# Update Fri, Jan  9, 2026  9:34:32 PM
// Logic update: Vd6SYhSW6Cmq
// Logic update: BdDGtkOrFF1l
// Logic update: OCNl04y1cTUa
// Logic update: xB2Y0qTPK7mY
// Logic update: LbKNeMbk5GgW
// Logic update: r447RcBmuLW0
