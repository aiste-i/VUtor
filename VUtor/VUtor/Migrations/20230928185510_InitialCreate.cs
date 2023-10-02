using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VUtor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTopic",
                columns: table => new
                {
                    TeachingProfilesProfileId = table.Column<int>(type: "int", nullable: false),
                    TopicsToTeachTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTopic", x => new { x.TeachingProfilesProfileId, x.TopicsToTeachTopicId });
                    table.ForeignKey(
                        name: "FK_ProfileTopic_Profiles_TeachingProfilesProfileId",
                        column: x => x.TeachingProfilesProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTopic_Topics_TopicsToTeachTopicId",
                        column: x => x.TopicsToTeachTopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTopic1",
                columns: table => new
                {
                    LearningProfilesProfileId = table.Column<int>(type: "int", nullable: false),
                    TopicsToLearnTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTopic1", x => new { x.LearningProfilesProfileId, x.TopicsToLearnTopicId });
                    table.ForeignKey(
                        name: "FK_ProfileTopic1_Profiles_LearningProfilesProfileId",
                        column: x => x.LearningProfilesProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTopic1_Topics_TopicsToLearnTopicId",
                        column: x => x.TopicsToLearnTopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopic_TopicsToTeachTopicId",
                table: "ProfileTopic",
                column: "TopicsToTeachTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTopic1_TopicsToLearnTopicId",
                table: "ProfileTopic1",
                column: "TopicsToLearnTopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileTopic");

            migrationBuilder.DropTable(
                name: "ProfileTopic1");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
