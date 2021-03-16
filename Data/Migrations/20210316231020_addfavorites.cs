using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addfavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AppUserId",
                table: "Books",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_AppUserId",
                table: "Books",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_AppUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AppUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Books");
        }
    }
}
