using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class FilesFoldersAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "UserFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserItems_AspNetUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserItems_UserFiles_UserFileId",
                        column: x => x.UserFileId,
                        principalTable: "UserFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderHierarchy",
                columns: table => new
                {
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    SubFoldersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderHierarchy", x => new { x.FolderId, x.SubFoldersId });
                    table.ForeignKey(
                        name: "FK_FolderHierarchy_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderHierarchy_Folders_SubFoldersId",
                        column: x => x.SubFoldersId,
                        principalTable: "Folders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_FolderId",
                table: "UserFiles",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderHierarchy_SubFoldersId",
                table: "FolderHierarchy",
                column: "SubFoldersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_ProfileId",
                table: "UserItems",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_UserFileId",
                table: "UserItems",
                column: "UserFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Folders_FolderId",
                table: "UserFiles",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Folders_FolderId",
                table: "UserFiles");

            migrationBuilder.DropTable(
                name: "FolderHierarchy");

            migrationBuilder.DropTable(
                name: "UserItems");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_UserFiles_FolderId",
                table: "UserFiles");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "UserFiles");
        }
    }
}
