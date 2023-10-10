using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class CourseNameAndYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseName",
                table: "AspNetUsers",
                type: "int",
                maxLength: 250,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseYear",
                table: "AspNetUsers",
                type: "int",
                maxLength: 250,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseYear",
                table: "AspNetUsers");
        }
    }
}
