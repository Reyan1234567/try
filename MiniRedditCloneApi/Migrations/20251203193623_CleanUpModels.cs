using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "nerds",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "UserName" });

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "herds",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name" });

            migrationBuilder.CreateIndex(
                name: "IX_nerds_SearchVector",
                table: "nerds",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_herds_SearchVector",
                table: "herds",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_nerds_SearchVector",
                table: "nerds");

            migrationBuilder.DropIndex(
                name: "IX_herds_SearchVector",
                table: "herds");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "nerds");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "herds");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:08 PM
# Update Fri, Jan  9, 2026  9:25:48 PM
# Update Fri, Jan  9, 2026  9:34:32 PM
