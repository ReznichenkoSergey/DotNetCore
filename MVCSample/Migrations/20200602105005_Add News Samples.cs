using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class AddNewsSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Title", "Text", "IsFake", "AuthorId" },
                values: new object[] {0, "Humanity finally colonized the Mercury!!", "", true, 10 });
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Title", "Text", "IsFake", "AuthorId" },
                values: new object[] { 1, "Increase your lifespan by 10 years, every morning you need...", "", true, 6 });
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Title", "Text", "IsFake", "AuthorId" },
                values: new object[] { 2, "Scientists estimed the time of the vaccine invension: it is a summer of 2021", "", false, 11 });
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Title", "Text", "IsFake", "AuthorId" },
                values: new object[] { 3, "Ukraine reduces the cost of its obligations!", "", false, 12 });
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Title", "Text", "IsFake", "AuthorId" },
                values: new object[] { 4, "A species were discovered in Africa: it is blue legless cat", "", true, 13 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
