using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBConnection.Migrations
{
    public partial class InitPostgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorsReadings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ax = table.Column<double>(nullable: true),
                    Ay = table.Column<double>(nullable: true),
                    Az = table.Column<double>(nullable: true),
                    Gx = table.Column<double>(nullable: true),
                    Gy = table.Column<double>(nullable: true),
                    Gz = table.Column<double>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    GameRotationVecX = table.Column<double>(nullable: true),
                    GameRotationVecY = table.Column<double>(nullable: true),
                    GameRotationVecZ = table.Column<double>(nullable: true),
                    GeomagneticRotationVecX = table.Column<double>(nullable: true),
                    GeomagneticRotationVecY = table.Column<double>(nullable: true),
                    GeomagneticRotationVecZ = table.Column<double>(nullable: true),
                    MagneticFieldX = table.Column<double>(nullable: true),
                    MagneticFieldY = table.Column<double>(nullable: true),
                    MagneticFieldZ = table.Column<double>(nullable: true),
                    GravityX = table.Column<double>(nullable: true),
                    GravityY = table.Column<double>(nullable: true),
                    GravityZ = table.Column<double>(nullable: true),
                    RotationVecX = table.Column<double>(nullable: true),
                    RotationVecY = table.Column<double>(nullable: true),
                    RotationVecZ = table.Column<double>(nullable: true),
                    Light = table.Column<double>(nullable: true),
                    StepDetector = table.Column<float>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    Proximity = table.Column<double>(nullable: true),
                    NumberOfSteps = table.Column<float>(nullable: true),
                    BatteryLevel = table.Column<int>(nullable: true),
                    ImInTram = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorsReadings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trams",
                columns: table => new
                {
                    TramId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CityId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trams", x => x.TramId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Length = table.Column<float>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Trams_TramId",
                        column: x => x.TramId,
                        principalTable: "Trams",
                        principalColumn: "TramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TramId",
                table: "Trips",
                column: "TramId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorsReadings");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Trams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
