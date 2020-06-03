using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class SeedWorldParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorldParts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Africa" },
                    { 2, "Eurasia" },
                    { 3, "North America" },
                    { 4, "South America" },
                    { 5, "Antarctica" },
                    { 6, "Australia" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorldParts",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
