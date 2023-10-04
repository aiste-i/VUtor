using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VUtor.Data.Migrations
{
    /// <inheritdoc />
    public partial class CustomFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTopicToLearn",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    ProfileId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTopicToLearn", x => new { x.ProfileId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_ProfileTopicToLearn_AspNetUsers_ProfileId1",
                        column: x => x.ProfileId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTopicToLearn_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTopicToTeach",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    ProfileId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTopicToTeach", x => new { x.ProfileId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_ProfileTopicToTeach_AspNetUsers_ProfileId1",
                        column: x => x.ProfileId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTopicToTeach_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopicToLearn_ProfileId1",
                table: "ProfileTopicToLearn",
                column: "ProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopicToLearn_TopicId",
                table: "ProfileTopicToLearn",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopicToTeach_ProfileId1",
                table: "ProfileTopicToTeach",
                column: "ProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopicToTeach_TopicId",
                table: "ProfileTopicToTeach",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileTopicToLearn");

            migrationBuilder.DropTable(
                name: "ProfileTopicToTeach");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
