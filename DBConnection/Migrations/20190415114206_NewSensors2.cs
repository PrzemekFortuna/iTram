using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class NewSensors2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GeomagneticRotationVecX",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GeomagneticRotationVecY",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GeomagneticRotationVecZ",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MagneticFieldX",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MagneticFieldY",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MagneticFieldZ",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NumberOfSteps",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "StepDetector",
                table: "SensorsReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeomagneticRotationVecX",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GeomagneticRotationVecY",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "GeomagneticRotationVecZ",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "MagneticFieldX",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "MagneticFieldY",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "MagneticFieldZ",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "NumberOfSteps",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "StepDetector",
                table: "SensorsReadings");
        }
    }
}
