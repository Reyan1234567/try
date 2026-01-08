using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedditCloneApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "nerds",
                newName: "UploadedAvatar");

            migrationBuilder.AddColumn<int>(
                name: "AvatarType",
                table: "nerds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DefaultAvatarNum",
                table: "nerds",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarType",
                table: "nerds");

            migrationBuilder.DropColumn(
                name: "DefaultAvatarNum",
                table: "nerds");

            migrationBuilder.RenameColumn(
                name: "UploadedAvatar",
                table: "nerds",
                newName: "Avatar");
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:06 PM
# Update Fri, Jan  9, 2026  9:25:46 PM
# Update Fri, Jan  9, 2026  9:34:31 PM
// Logic update: L06s5ATrRL6B
// Logic update: TZTMPNMqj1oo
// Logic update: gWzhm26duMtI
// Logic update: UZ4O2d8aSq37
// Logic update: EHAgeg5JrtX9
// Logic update: ktQawaePWgV6
