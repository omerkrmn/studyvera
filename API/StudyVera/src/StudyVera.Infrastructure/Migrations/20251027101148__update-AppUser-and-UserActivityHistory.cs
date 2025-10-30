using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _updateAppUserandUserActivityHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityHistories_AspNetUsers_AppUserId",
                table: "UserActivityHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityHistories_AppUserId",
                table: "UserActivityHistories");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserActivityHistories");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityHistories_UserId",
                table: "UserActivityHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityHistories_AspNetUsers_UserId",
                table: "UserActivityHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityHistories_AspNetUsers_UserId",
                table: "UserActivityHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityHistories_UserId",
                table: "UserActivityHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "UserActivityHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityHistories_AppUserId",
                table: "UserActivityHistories",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivityHistories_AspNetUsers_AppUserId",
                table: "UserActivityHistories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
