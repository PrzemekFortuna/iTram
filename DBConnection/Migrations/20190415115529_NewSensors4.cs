using Microsoft.EntityFrameworkCore.Migrations;

namespace DBConnection.Migrations
{
    public partial class NewSensors4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RotationVecX",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RotationVecY",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RotationVecZ",
                table: "SensorsReadings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RotationVecX",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "RotationVecY",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "RotationVecZ",
                table: "SensorsReadings");
        }
    }
}
