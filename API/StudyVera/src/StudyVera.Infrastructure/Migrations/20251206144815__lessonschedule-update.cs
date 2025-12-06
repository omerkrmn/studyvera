using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _lessonscheduleupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonSchedules_UserId",
                table: "LessonSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_LessonSchedules_UserId",
                table: "LessonSchedules",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonSchedules_UserId",
                table: "LessonSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_LessonSchedules_UserId",
                table: "LessonSchedules",
                column: "UserId",
                unique: true);
        }
    }
}
