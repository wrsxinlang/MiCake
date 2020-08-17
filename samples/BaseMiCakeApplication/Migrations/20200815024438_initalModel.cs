using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseMiCakeApplication.Migrations
{
    public partial class initalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookName = table.Column<string>(nullable: true),
                    Author_FirstName = table.Column<string>(nullable: true),
                    Author_LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checker",
                columns: table => new
                {
                    CheckerID = table.Column<Guid>(nullable: false),
                    CheckerName = table.Column<string>(nullable: true),
                    ChecherAct = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checker", x => x.CheckerID);
                });

            migrationBuilder.CreateTable(
                name: "Itinerary",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    NoteTime = table.Column<DateTime>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mlcy_Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RelationObjectID = table.Column<Guid>(nullable: false),
                    ReplayType = table.Column<int>(nullable: false),
                    CreateUserObjectID = table.Column<Guid>(nullable: false),
                    ReplyContent = table.Column<string>(nullable: true),
                    ReplyToUserObjectID = table.Column<Guid>(nullable: true),
                    ParentID = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mlcy_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_File",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    FileLength = table.Column<long>(nullable: false),
                    FileExtention = table.Column<string>(nullable: true),
                    FileType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Reputation = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserWechat",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<Guid>(nullable: false),
                    WeChatOpenID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserWechat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mlcy_NewIdeas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Introduce = table.Column<string>(maxLength: 250, nullable: true),
                    Graphic = table.Column<string>(nullable: true),
                    RelationId = table.Column<Guid>(nullable: false),
                    CoverImgId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    WorksID = table.Column<Guid>(nullable: true),
                    CommentCount = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    PublishCount = table.Column<int>(nullable: false),
                    IsOtherEdit = table.Column<bool>(nullable: false),
                    CreateUserID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    CheckerID = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsChecked = table.Column<bool>(nullable: false),
                    CheckedMsg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mlcy_NewIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mlcy_NewIdeas_Checker_CheckerID",
                        column: x => x.CheckerID,
                        principalTable: "Checker",
                        principalColumn: "CheckerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mlcy_NewIdeas_Sys_File_CoverImgId",
                        column: x => x.CoverImgId,
                        principalTable: "Sys_File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mlcy_NewIdeas_CheckerID",
                table: "Mlcy_NewIdeas",
                column: "CheckerID");

            migrationBuilder.CreateIndex(
                name: "IX_Mlcy_NewIdeas_CoverImgId",
                table: "Mlcy_NewIdeas",
                column: "CoverImgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Itinerary");

            migrationBuilder.DropTable(
                name: "Mlcy_Comments");

            migrationBuilder.DropTable(
                name: "Mlcy_NewIdeas");

            migrationBuilder.DropTable(
                name: "Sys_User");

            migrationBuilder.DropTable(
                name: "Sys_UserWechat");

            migrationBuilder.DropTable(
                name: "Checker");

            migrationBuilder.DropTable(
                name: "Sys_File");
        }
    }
}
