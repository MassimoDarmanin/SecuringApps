using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuringApps.Migrations
{
    public partial class fileUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.AddColumn<byte>(
                name: "Attachment",
                table: "Files",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
