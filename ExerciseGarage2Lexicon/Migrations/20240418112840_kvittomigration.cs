using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseGarage2Lexicon.Migrations
{
    /// <inheritdoc />
    public partial class kvittomigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutTime",
                table: "ParkedVehicle",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ParkedVehicle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalParkingTimeInMinutes",
                table: "ParkedVehicle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "TotalParkingTimeInMinutes",
                table: "ParkedVehicle");
        }
    }
}
