using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _changeUserLessonProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLessonProgresses_AspNetUsers_AppUserId",
                table: "UserLessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserLessonProgresses_AppUserId",
                table: "UserLessonProgresses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserLessonProgresses");

            migrationBuilder.CreateIndex(
                name: "IX_UserLessonProgresses_UserId",
                table: "UserLessonProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessonProgresses_AspNetUsers_UserId",
                table: "UserLessonProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLessonProgresses_AspNetUsers_UserId",
                table: "UserLessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserLessonProgresses_UserId",
                table: "UserLessonProgresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "UserLessonProgresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLessonProgresses_AppUserId",
                table: "UserLessonProgresses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessonProgresses_AspNetUsers_AppUserId",
                table: "UserLessonProgresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
