using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserItemAnUserFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_UserFiles_UserFileId",
                table: "UserItems");

            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_UserFileId",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "UserFileId",
                table: "UserItems");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "UserItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UserItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "UserItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_FolderId",
                table: "UserItems",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_TopicId",
                table: "UserItems",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Folders_FolderId",
                table: "UserItems",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Topics_TopicId",
                table: "UserItems",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Folders_FolderId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Topics_TopicId",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_FolderId",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_TopicId",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "UserItems");

            migrationBuilder.AddColumn<int>(
                name: "UserFileId",
                table: "UserItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFiles_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFiles_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_UserFileId",
                table: "UserItems",
                column: "UserFileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_FolderId",
                table: "UserFiles",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_TopicId",
                table: "UserFiles",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_UserFiles_UserFileId",
                table: "UserItems",
                column: "UserFileId",
                principalTable: "UserFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
