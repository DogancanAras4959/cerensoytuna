using Microsoft.EntityFrameworkCore.Migrations;

namespace cerensoytuna.DAL.Migrations
{
    public partial class vLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "postlanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postTrTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    postEngTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postlanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postlanguage_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_postlanguage_PostId",
                table: "postlanguage",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postlanguage");
        }
    }
}
