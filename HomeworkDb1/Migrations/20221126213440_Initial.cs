using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkDb1.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lectors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false),
                    courseid = table.Column<Guid>(name: "course_id", type: "uuid", nullable: true),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lectors", x => x.id);
                    table.ForeignKey(
                        name: "fk_lectors_courses_course_id",
                        column: x => x.courseid,
                        principalTable: "courses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "course_student",
                columns: table => new
                {
                    coursesid = table.Column<Guid>(name: "courses_id", type: "uuid", nullable: false),
                    studentsid = table.Column<Guid>(name: "students_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_student", x => new { x.coursesid, x.studentsid });
                    table.ForeignKey(
                        name: "fk_course_student_courses_courses_id",
                        column: x => x.coursesid,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_course_student_students_students_id",
                        column: x => x.studentsid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_student_students_id",
                table: "course_student",
                column: "students_id");

            migrationBuilder.CreateIndex(
                name: "ix_lectors_course_id",
                table: "lectors",
                column: "course_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_student");

            migrationBuilder.DropTable(
                name: "lectors");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
