using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class OnModelCreatingDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Humans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "DeadCount", "Name", "Population", "RecoveredCount", "SickCount", "Vaccine" },
                values: new object[] { 1, 97811, "US", 328200000, 376266, 1647741, false });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 1, 38, 1, "Obi-wan", "Male", false, "Kenobi" });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 2, 54, 1, "Sanwise", "Male", false, "Gamgee" });
        }
    }
}
