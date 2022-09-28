using Microsoft.EntityFrameworkCore.Migrations;

namespace cerensoytuna.DAL.Migrations
{
    public partial class editDatabaseUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAdress",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAdress",
                table: "users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }
    }
}
