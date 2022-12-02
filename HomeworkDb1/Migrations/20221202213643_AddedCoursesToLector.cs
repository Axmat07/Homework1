using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkDb1.Migrations
{
    public partial class AddedCoursesToLector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_lectors_courses_course_id",
                table: "lectors");

            migrationBuilder.DropIndex(
                name: "ix_lectors_course_id",
                table: "lectors");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "lectors");

            migrationBuilder.CreateTable(
                name: "course_lector",
                columns: table => new
                {
                    courses_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lectors_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_lector", x => new { x.courses_id, x.lectors_id });
                    table.ForeignKey(
                        name: "fk_course_lector_courses_courses_id",
                        column: x => x.courses_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_lector_lectors_lectors_id",
                        column: x => x.lectors_id,
                        principalTable: "lectors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_lector_lectors_id",
                table: "course_lector",
                column: "lectors_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_lector");

            migrationBuilder.AddColumn<Guid>(
                name: "course_id",
                table: "lectors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_lectors_course_id",
                table: "lectors",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "fk_lectors_courses_course_id",
                table: "lectors",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id");
        }
    }
}
