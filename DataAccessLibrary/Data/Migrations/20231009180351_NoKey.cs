using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class NoKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilesTeachingTopics",
                table: "ProfilesTeachingTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilesLearningTopics",
                table: "ProfilesLearningTopics");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilesTeachingTopics_TeachingProfilesId",
                table: "ProfilesTeachingTopics",
                column: "TeachingProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilesLearningTopics_LearningProfilesId",
                table: "ProfilesLearningTopics",
                column: "LearningProfilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfilesTeachingTopics_TeachingProfilesId",
                table: "ProfilesTeachingTopics");

            migrationBuilder.DropIndex(
                name: "IX_ProfilesLearningTopics_LearningProfilesId",
                table: "ProfilesLearningTopics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilesTeachingTopics",
                table: "ProfilesTeachingTopics",
                columns: new[] { "TeachingProfilesId", "TopicsToTeachId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilesLearningTopics",
                table: "ProfilesLearningTopics",
                columns: new[] { "LearningProfilesId", "TopicsToLearnId" });
        }
    }
}
