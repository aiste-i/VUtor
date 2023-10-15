using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Struct_Record_Ammendment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseYear",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CourseInfo",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreationDate",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

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
    }
}
