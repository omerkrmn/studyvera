using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_UserActivityHistoryTable_added_2_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "UserActivityHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "UserActivityHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_UserId",
                table: "StudySessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_AspNetUsers_UserId",
                table: "StudySessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_AspNetUsers_UserId",
                table: "StudySessions");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_UserId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "UserActivityHistories");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "UserActivityHistories");
        }
    }
}
