using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2_0.Migrations
{
    public partial class initialAtHome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkedIn",
                value: new DateTime(2019, 5, 26, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkedIn",
                value: new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
