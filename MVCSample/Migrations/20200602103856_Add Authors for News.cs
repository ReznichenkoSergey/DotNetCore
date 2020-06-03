using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class AddAuthorsforNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 60, 5, "Jeremy", "Male", false, "Clarkson" });
            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 33, 1, "John", "Male", false, "Jones" });
            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 18, 1, "Cerol", "Female", false, "Denvers" });
            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Age", "CountryId", "FirstName", "Gender", "IsSick", "LastName" },
                values: new object[] { 46, 1, "Jimmy", "Male", false, "Felon" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
