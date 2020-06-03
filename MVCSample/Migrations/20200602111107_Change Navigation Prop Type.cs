using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCSample.Migrations
{
    public partial class ChangeNavigationPropType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Humans_News_NewsId",
                table: "Humans");

            migrationBuilder.DropIndex(
                name: "IX_Humans_NewsId",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Humans");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Humans_AuthorId",
                table: "News",
                column: "AuthorId",
                principalTable: "Humans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Humans_AuthorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_AuthorId",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Humans",
                type: "int",
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
    }
}
