using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class NewSensors3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GravityX",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GravityY",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GravityZ",
                table: "SensorsReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GravityX",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GravityY",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GravityZ",
                table: "SensorsReadings");
        }
    }
}
