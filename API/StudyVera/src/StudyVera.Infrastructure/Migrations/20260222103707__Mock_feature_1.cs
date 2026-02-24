using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _Mock_feature_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamQuestionCount",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserMockExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalNet = table.Column<double>(type: "float", nullable: false),
                    TotalCorrect = table.Column<int>(type: "int", nullable: false),
                    TotalWrong = table.Column<int>(type: "int", nullable: false),
                    TotalEmpty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMockExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMockExams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMockExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMockExamDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMockExamId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    WrongCount = table.Column<int>(type: "int", nullable: false),
                    EmptyCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMockExamDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMockExamDetails_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMockExamDetails_UserMockExams_UserMockExamId",
                        column: x => x.UserMockExamId,
                        principalTable: "UserMockExams",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                column: "ExamQuestionCount",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMockExamDetails_LessonId",
                table: "UserMockExamDetails",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMockExamDetails_UserMockExamId",
                table: "UserMockExamDetails",
                column: "UserMockExamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMockExams_ExamId",
                table: "UserMockExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMockExams_UserId",
                table: "UserMockExams",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMockExamDetails");

            migrationBuilder.DropTable(
                name: "UserMockExams");

            migrationBuilder.DropColumn(
                name: "ExamQuestionCount",
                table: "Lessons");
        }
    }
}
