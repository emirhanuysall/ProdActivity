using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdActivity.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LoggerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_UserId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Logs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "Logs",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "Logs",
                newName: "LogType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Logs",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "LogType",
                table: "Logs",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Logs",
                newName: "Timestamp");

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
