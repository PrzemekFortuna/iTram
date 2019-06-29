using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class VehicleLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleLatitude",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleLongitude",
                table: "SensorsReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleLatitude",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "VehicleLongitude",
                table: "SensorsReadings");
        }
    }
}
