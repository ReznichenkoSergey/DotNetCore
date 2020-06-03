using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class AddAuthorNavigatonProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Humans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Humans_NewsId",
                table: "Humans",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Humans_News_NewsId",
                table: "Humans",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Humans_News_NewsId",
                table: "Humans");

            migrationBuilder.DropIndex(
                name: "IX_Humans_NewsId",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Humans");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
