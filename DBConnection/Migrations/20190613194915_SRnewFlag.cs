using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class SRnewFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "SensorsReadings");
        }
    }
}
