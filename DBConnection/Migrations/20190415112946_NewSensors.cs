using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class NewSensors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GameRotationVecX",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GameRotationVecY",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GameRotationVecZ",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImInTram",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Light",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Proximity",
                table: "SensorsReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameRotationVecX",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GameRotationVecY",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GameRotationVecZ",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "ImInTram",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Light",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "Proximity",
                table: "SensorsReadings");
        }
    }
}
