using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkDb1.Migrations
{
    public partial class AddedNameFiealdToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "courses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "courses");
        }
    }
}
