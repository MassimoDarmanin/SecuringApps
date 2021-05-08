using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuringApps.Migrations
{
    public partial class DisableBoolTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disable",
                table: "Taskeds",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disable",
                table: "Taskeds");
        }
    }
}
