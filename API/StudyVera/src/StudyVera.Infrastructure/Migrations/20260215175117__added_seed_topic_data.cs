using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyVera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _added_seed_topic_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "LessonId", "Name", "OrderIndex", "Priority" },
                values: new object[,]
                {
                    { 1, 2, "Sözcükte Anlam", 1, (byte)3 },
                    { 2, 2, "Cümlede Anlam", 2, (byte)3 },
                    { 3, 2, "Sözcük Türleri", 3, (byte)3 },
                    { 4, 2, "Sözcükte Yapı", 4, (byte)3 },
                    { 5, 2, "Cümlenin Ögeleri", 5, (byte)3 },
                    { 6, 2, "Cümle Türleri", 6, (byte)3 },
                    { 7, 2, "Dil Bilgisi Ses Olayları", 7, (byte)3 },
                    { 8, 2, "Yazım Kuralları", 8, (byte)3 },
                    { 9, 2, "Noktalama İşaretleri", 9, (byte)3 },
                    { 10, 2, "Anlatım Bozuklukları", 10, (byte)3 },
                    { 11, 2, "Paragrafta Anlam", 11, (byte)3 },
                    { 12, 2, "Paragrafta Anlatım Biçimi", 12, (byte)3 },
                    { 13, 2, "Sözel Mantık", 13, (byte)3 },
                    { 14, 5, "Temel Kavramlar", 1, (byte)3 },
                    { 15, 5, "Rasyonel Sayılar - Ondalıklı Sayılar", 2, (byte)3 },
                    { 16, 5, "Basit Eşitsizlikler", 3, (byte)3 },
                    { 17, 5, "Mutlak Değer", 4, (byte)3 },
                    { 18, 5, "Üslü Sayılar", 5, (byte)3 },
                    { 19, 5, "Köklü Sayılar", 6, (byte)3 },
                    { 20, 5, "Çarpanlara Ayırma", 7, (byte)3 },
                    { 21, 5, "Oran-Orantı", 8, (byte)3 },
                    { 22, 5, "Denklem Çözme", 9, (byte)3 },
                    { 23, 5, "Problemler", 10, (byte)3 },
                    { 24, 5, "Kümeler", 11, (byte)3 },
                    { 25, 5, "Fonksiyonlar", 12, (byte)3 },
                    { 26, 5, "İşlem", 13, (byte)3 },
                    { 27, 5, "Permütasyon", 14, (byte)3 },
                    { 28, 5, "Kombinasyon", 15, (byte)3 },
                    { 29, 5, "Olasılık", 16, (byte)3 },
                    { 30, 5, "Sayısal Mantık", 17, (byte)3 },
                    { 31, 8, "Geometrik Kavramlar ve Açılar", 1, (byte)3 },
                    { 32, 8, "Çokgenler ve Dörtgenler", 2, (byte)3 },
                    { 33, 8, "Çember ve Daire", 3, (byte)3 },
                    { 34, 8, "Analitik Geometri", 4, (byte)3 },
                    { 35, 8, "Katı Cisimler", 5, (byte)3 },
                    { 36, 17, "İslamiyet Öncesi Türk Tarihi", 1, (byte)3 },
                    { 37, 17, "İlk Türk-İslam Devletleri ve Beylikleri", 2, (byte)3 },
                    { 38, 17, "Osmanlı Devleti Kuruluş ve Yükselme Dönemleri", 3, (byte)3 },
                    { 39, 17, "Osmanlı Devleti'nde Kültür ve Uygarlık", 4, (byte)3 },
                    { 40, 17, "XVII. Yüzyılda Osmanlı Devleti (Duraklama)", 5, (byte)3 },
                    { 41, 17, "XVIII. Yüzyılda Osmanlı Devleti (Gerileme)", 6, (byte)3 },
                    { 42, 17, "XIX. Yüzyılda Osmanlı Devleti (Dağılma)", 7, (byte)3 },
                    { 43, 17, "XX. Yüzyılda Osmanlı Devleti", 8, (byte)3 },
                    { 44, 17, "Kurtuluş Savaşı Hazırlık Dönemi", 9, (byte)3 },
                    { 45, 17, "I. TBMM Dönemi", 10, (byte)3 },
                    { 46, 17, "Kurtuluş Savaşı Muharebeler Dönemi", 11, (byte)3 },
                    { 47, 17, "Atatürk İnkılapları", 12, (byte)3 },
                    { 48, 17, "Atatürk İlkeleri", 13, (byte)3 },
                    { 49, 17, "Partiler ve Partileşme Dönemi", 14, (byte)0 },
                    { 50, 17, "Atatürk Dönemi Türk Dış Politikası", 15, (byte)3 },
                    { 51, 17, "Atatürk Sonrası Dönem", 16, (byte)3 },
                    { 52, 17, "Atatürk'ün Hayatı ve Kişiliği", 17, (byte)3 },
                    { 53, 20, "Türkiye'nin Coğrafi Konumu", 1, (byte)3 },
                    { 54, 20, "Türkiye'nin İklimi ve Bitki Örtüsü", 2, (byte)3 },
                    { 55, 20, "Türkiye'nin Fiziki Özellikleri", 3, (byte)3 },
                    { 56, 20, "Türkiye'de Nüfus ve Yerleşme", 4, (byte)3 },
                    { 57, 20, "Tarım", 5, (byte)3 },
                    { 58, 20, "Hayvancılık", 6, (byte)3 },
                    { 59, 20, "Madenler ve Enerji Kaynakları", 7, (byte)3 },
                    { 60, 20, "Sanayi ve Endüstri", 8, (byte)3 },
                    { 61, 20, "Ulaşım", 9, (byte)3 },
                    { 62, 20, "Ticaret", 10, (byte)3 },
                    { 63, 20, "Turizm", 11, (byte)3 },
                    { 64, 20, "Bölgeler Coğrafyası", 12, (byte)3 },
                    { 65, 26, "Temel Hukuk Kavramları", 1, (byte)3 },
                    { 66, 26, "Anayasal Kavramlar", 2, (byte)3 },
                    { 67, 26, "Türk Anayasa Tarihi", 3, (byte)3 },
                    { 68, 26, "Temel Hak ve Ödevler", 4, (byte)3 },
                    { 69, 26, "Yasama", 5, (byte)3 },
                    { 70, 26, "Yürütme", 6, (byte)3 },
                    { 71, 26, "Yargı", 7, (byte)3 },
                    { 72, 26, "İdare Hukuku", 8, (byte)3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 72);
        }
    }
}
