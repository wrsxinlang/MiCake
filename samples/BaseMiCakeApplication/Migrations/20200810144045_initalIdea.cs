using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseMiCakeApplication.Migrations
{
    public partial class initalIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mlcy_NewIdeas_Checker_CheckerID",
                table: "Mlcy_NewIdeas");

            migrationBuilder.DropColumn(
                name: "ChecherAct",
                table: "Mlcy_NewIdeas");

            migrationBuilder.DropColumn(
                name: "CheckerName",
                table: "Mlcy_NewIdeas");

            migrationBuilder.AlterColumn<Guid>(
                name: "CheckerID",
                table: "Mlcy_NewIdeas",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Mlcy_NewIdeas_Checker_CheckerID",
                table: "Mlcy_NewIdeas",
                column: "CheckerID",
                principalTable: "Checker",
                principalColumn: "CheckerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mlcy_NewIdeas_Checker_CheckerID",
                table: "Mlcy_NewIdeas");

            migrationBuilder.AlterColumn<Guid>(
                name: "CheckerID",
                table: "Mlcy_NewIdeas",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChecherAct",
                table: "Mlcy_NewIdeas",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckerName",
                table: "Mlcy_NewIdeas",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mlcy_NewIdeas_Checker_CheckerID",
                table: "Mlcy_NewIdeas",
                column: "CheckerID",
                principalTable: "Checker",
                principalColumn: "CheckerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
