using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cerensoytuna.DAL.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LangId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    langTitle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_LangId",
                table: "Post",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_language_LangId",
                table: "Post",
                column: "LangId",
                principalTable: "language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_language_LangId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropIndex(
                name: "IX_Post_LangId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LangId",
                table: "Post");
        }
    }
}
