using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNsfwOptionField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NsfwOption",
                table: "nerds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "nsfw_confidence",
                table: "insights",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NsfwOption",
                table: "nerds");

            migrationBuilder.AlterColumn<int>(
                name: "nsfw_confidence",
                table: "insights",
                type: "integer",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:11 PM
# Update Fri, Jan  9, 2026  9:25:51 PM
# Update Fri, Jan  9, 2026  9:34:35 PM
