using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library2.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_id",
                table: "borrowed_books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_id",
                table: "borrowed_books");
        }
    }
}
