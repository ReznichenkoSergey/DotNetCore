using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class AddSomePersons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "DeadCount", "Name", "Population", "RecoveredCount", "SickCount", "Vaccine" },
                values: new object[] { 1, 100103, "US", 328200000, 469049, 1714327, false });
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "DeadCount", "Name", "Population", "RecoveredCount", "SickCount", "Vaccine" },
                values: new object[] { 4, 644, "Ukraine", 42000000, 7575, 21584, false });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 8, 55, 4, "Petr", "Male", false, "Poroshenko" });
            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 9, 45, 4, "Oleg", "Female", false, "Lyashko" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Humans",
                keyColumn: "Id",
                keyValue: 8);
            migrationBuilder.DeleteData(
                table: "Humans",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
