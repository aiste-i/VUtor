using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VUtor.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfilesAndTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileTopicToLearn");

            migrationBuilder.DropTable(
                name: "ProfileTopicToTeach");

            migrationBuilder.CreateTable(
                name: "ProfilesLearningTopics",
                columns: table => new
                {
                    LearningProfilesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TopicsToLearnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilesLearningTopics", x => new { x.LearningProfilesId, x.TopicsToLearnId });
                    table.ForeignKey(
                        name: "FK_ProfilesLearningTopics_AspNetUsers_LearningProfilesId",
                        column: x => x.LearningProfilesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilesLearningTopics_Topics_TopicsToLearnId",
                        column: x => x.TopicsToLearnId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilesTeachingTopics",
                columns: table => new
                {
                    TeachingProfilesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TopicsToTeachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilesTeachingTopics", x => new { x.TeachingProfilesId, x.TopicsToTeachId });
                    table.ForeignKey(
                        name: "FK_ProfilesTeachingTopics_AspNetUsers_TeachingProfilesId",
                        column: x => x.TeachingProfilesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilesTeachingTopics_Topics_TopicsToTeachId",
                        column: x => x.TopicsToTeachId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilesLearningTopics_TopicsToLearnId",
                table: "ProfilesLearningTopics",
                column: "TopicsToLearnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilesTeachingTopics_TopicsToTeachId",
                table: "ProfilesTeachingTopics",
                column: "TopicsToTeachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilesLearningTopics");

            migrationBuilder.DropTable(
                name: "ProfilesTeachingTopics");

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
    }
}
