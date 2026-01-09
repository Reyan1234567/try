using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInsightsForModeration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "media_url",
                table: "insights",
                newName: "removed_reason");

            migrationBuilder.AddColumn<bool>(
                name: "is_auto_flagged_nsfw",
                table: "insights",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "media",
                table: "insights",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "nsfw_confidence",
                table: "insights",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "removed_at",
                table: "insights",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "insights",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_auto_flagged_nsfw",
                table: "insights");

            migrationBuilder.DropColumn(
                name: "media",
                table: "insights");

            migrationBuilder.DropColumn(
                name: "nsfw_confidence",
                table: "insights");

            migrationBuilder.DropColumn(
                name: "removed_at",
                table: "insights");

            migrationBuilder.DropColumn(
                name: "status",
                table: "insights");

            migrationBuilder.RenameColumn(
                name: "removed_reason",
                table: "insights",
                newName: "media_url");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:11 PM
# Update Fri, Jan  9, 2026  9:25:50 PM
# Update Fri, Jan  9, 2026  9:34:35 PM
