using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class SensorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "C",
                table: "SensorsReadings",
                newName: "Longtitude");

            migrationBuilder.RenameColumn(
                name: "B",
                table: "SensorsReadings",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "A",
                table: "SensorsReadings",
                newName: "Gz");

            migrationBuilder.AddColumn<double>(
                name: "Ax",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Ay",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Az",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevel",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Gx",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Gy",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ax",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Ay",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Az",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "BatteryLevel",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Gx",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Gy",
                table: "SensorsReadings");

            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "SensorsReadings",
                newName: "C");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "SensorsReadings",
                newName: "B");

            migrationBuilder.RenameColumn(
                name: "Gz",
                table: "SensorsReadings",
                newName: "A");
        }
    }
}
