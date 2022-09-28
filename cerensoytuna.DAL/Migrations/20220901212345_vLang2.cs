using Microsoft.EntityFrameworkCore.Migrations;

namespace cerensoytuna.DAL.Migrations
{
    public partial class vLang2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postlanguage_Post_PostId",
                table: "postlanguage");

            migrationBuilder.DropIndex(
                name: "IX_postlanguage_PostId",
                table: "postlanguage");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "postlanguage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "postlanguage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_postlanguage_PostId",
                table: "postlanguage",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_postlanguage_Post_PostId",
                table: "postlanguage",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
