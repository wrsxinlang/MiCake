using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseMiCakeApplication.Migrations
{
    public partial class addNewIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checker",
                columns: table => new
                {
                    CheckerID = table.Column<Guid>(nullable: false),
                    CheckerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checker", x => x.CheckerID);
                });

            migrationBuilder.CreateTable(
                name: "NewIdeas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Introduce = table.Column<string>(nullable: true),
                    Graphic = table.Column<string>(nullable: true),
                    RelationId = table.Column<Guid>(nullable: false),
                    CoverImgId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    WorksID = table.Column<Guid>(nullable: true),
                    IsOtherEdit = table.Column<bool>(nullable: false),
                    CheckerID = table.Column<Guid>(nullable: true),
                    CheckedTime = table.Column<DateTime>(nullable: true),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreateUserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewIdeas_Checker_CheckerID",
                        column: x => x.CheckerID,
                        principalTable: "Checker",
                        principalColumn: "CheckerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewIdeas_Sys_File_CoverImgId",
                        column: x => x.CoverImgId,
                        principalTable: "Sys_File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewIdeas_CheckerID",
                table: "NewIdeas",
                column: "CheckerID");

            migrationBuilder.CreateIndex(
                name: "IX_NewIdeas_CoverImgId",
                table: "NewIdeas",
                column: "CoverImgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewIdeas");

            migrationBuilder.DropTable(
                name: "Checker");
        }
    }
}
