using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _deleteforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivityHistories_AspNetUsers_UserId",
                table: "UserActivityHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserActivityHistories_UserId",
                table: "UserActivityHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
