using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _Add_Friendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExamDate", "Name" },
                values: new object[] { "Kamu Personeli Seçme Sınavı", new DateTime(2026, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "KPSS" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                column: "ExamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                column: "ExamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                column: "ExamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                column: "ExamId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendships",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_RequestorId_ReceiverId",
                table: "Friendships",
                columns: new[] { "RequestorId", "ReceiverId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.UpdateData(
                table: "Exams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExamDate", "Name" },
                values: new object[] { "Yükseköğretim Kurumları Sınavı", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "TYT" });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "Description", "ExamDate", "Name" },
                values: new object[,]
                {
                    { 2, "Yükseköğretim Kurumları Sınavı", new DateTime(2026, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "AYT" },
                    { 3, "Dikey Geçiş Sınavı", new DateTime(2026, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "DGS" },
                    { 4, "Kamu Personeli Seçme Sınavı", new DateTime(2026, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "KPSS" },
                    { 5, "Akademik Personel ve Lisansüstü Eğitimi Giriş Sınavı", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ALES" }
                });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExamId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExamId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                column: "ExamId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                column: "ExamId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                column: "ExamId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                column: "ExamId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "ExamId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Türkçe" },
                    { 3, 1, "Matematik" },
                    { 6, 1, "Geometri" },
                    { 9, 1, "Fizik" },
                    { 11, 1, "Kimya" },
                    { 13, 1, "Biyoloji" },
                    { 15, 1, "Tarih" },
                    { 18, 1, "Çoğrafya" },
                    { 21, 1, "Felsefe" },
                    { 23, 1, "Din Kültürü ve Ahlak Bilgisi Konuları" },
                    { 4, 2, "Matematik" },
                    { 7, 2, "Geometri" },
                    { 10, 2, "Fizik" },
                    { 12, 2, "Kimya" },
                    { 14, 2, "Biyoloji" },
                    { 16, 2, "Tarih" },
                    { 19, 2, "Çoğrafya" },
                    { 22, 2, "Felsefe" },
                    { 24, 2, "Din Kültürü ve Ahlak Bilgisi Konuları" },
                    { 25, 2, "Edebiyat" }
                });
        }
    }
}
