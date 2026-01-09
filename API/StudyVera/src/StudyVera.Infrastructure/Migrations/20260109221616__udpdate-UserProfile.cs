using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _udpdateUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RankResults");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSettingsId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "AllowFriendRequests",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentTitle",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DailyReminderHour",
                table: "UserSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyStudyMinuteGoal",
                table: "UserSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfilePublic",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ShowRankInLeaderboard",
                table: "UserSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "WeeklyQuestionGoal",
                table: "UserSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_AspNetUsers_UserId",
                table: "UserSettings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_AspNetUsers_UserId",
                table: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "AllowFriendRequests",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CurrentTitle",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "DailyReminderHour",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "DailyStudyMinuteGoal",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "IsProfilePublic",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "ShowRankInLeaderboard",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "WeeklyQuestionGoal",
                table: "UserSettings");

            migrationBuilder.AddColumn<int>(
                name: "UserSettingsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RankResults",
                columns: table => new
                {
                    UserRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserSettingsId",
                table: "AspNetUsers",
                column: "UserSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserSettings_UserSettingsId",
                table: "AspNetUsers",
                column: "UserSettingsId",
                principalTable: "UserSettings",
                principalColumn: "Id");
        }
    }
}
