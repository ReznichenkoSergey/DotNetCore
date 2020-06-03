using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class AddWorldParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorldPartId",
                table: "Countries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorldParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldParts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_WorldPartId",
                table: "Countries",
                column: "WorldPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_WorldParts_WorldPartId",
                table: "Countries",
                column: "WorldPartId",
                principalTable: "WorldParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_WorldParts_WorldPartId",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "WorldParts");

            migrationBuilder.DropIndex(
                name: "IX_Countries_WorldPartId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "WorldPartId",
                table: "Countries");
        }
    }
}
