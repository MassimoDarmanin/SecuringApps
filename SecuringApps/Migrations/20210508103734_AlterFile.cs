using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuringApps.Migrations
{
    public partial class AlterFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "FileModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "FileModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "FileModel");
        }
    }
}
