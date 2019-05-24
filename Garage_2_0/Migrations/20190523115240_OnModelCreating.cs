using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_2_0.Migrations
{
    public partial class OnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "Color", "FreeText", "Model", "NoWheels", "ParkedIn", "ParkedOut", "RegNr", "Type" },
                values: new object[] { 1, "Volvo", "Grön", "Hejsan", "Amazon", 4, new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), "ABC123", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
