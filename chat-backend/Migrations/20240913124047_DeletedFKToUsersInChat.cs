using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat_backend.Migrations
{
    /// <inheritdoc />
    public partial class DeletedFKToUsersInChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_chatId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_chatId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "chatId",
                table: "Users");

            migrationBuilder.AddColumn<List<string>>(
                name: "usersId",
                table: "Chats",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usersId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "chatId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_chatId",
                table: "Users",
                column: "chatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_chatId",
                table: "Users",
                column: "chatId",
                principalTable: "Chats",
                principalColumn: "chatId");
        }
    }
}
