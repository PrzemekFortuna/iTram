using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBConnection.Migrations
{
    public partial class AddIdAndTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NearestBeaconId",
                table: "SensorsReadings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SensorsReadings",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trams_CityId",
                table: "Trams",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorsReadings_UserId",
                table: "SensorsReadings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorsReadings_Users_UserId",
                table: "SensorsReadings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trams_Cities_CityId",
                table: "Trams",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorsReadings_Users_UserId",
                table: "SensorsReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trams_Cities_CityId",
                table: "Trams");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Trams_CityId",
                table: "Trams");

            migrationBuilder.DropIndex(
                name: "IX_SensorsReadings_UserId",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "NearestBeaconId",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "SensorsReadings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SensorsReadings");
        }
    }
}
