using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "Population", "SickCount", "DeadCount", "RecoveredCount", "Vaccine" },
                values: new object[] { 2, "Russia", 144500000, 299941, 2837, 76130, false });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "Population", "SickCount", "DeadCount", "RecoveredCount", "Vaccine" },
                values: new object[] { 3, "Brazil", 209500000, 271628, 17971, 100459, false });



            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 3, "Hose", "Rodriges", 30, true, "Male", 3 });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 4, "Consuela", "Tridana", 43, false, "Female", 3 });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 5, "Ana", "Cormelia", 25, true, "Female", 3 });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 6, "Svetlana", "Sokolova", 44, false, "Female", 2 });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 7, "Petr", "Ilich", 53, true, "Male", 2 });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "FirstName", "LastName", "Age", "IsSick", "Gender", "CountryId" },
                values: new object[] { 8, "Thomas", "Edison", 84, true, "Male", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
