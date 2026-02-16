using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Configurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasData(
        #region KPSS
            //Türkçe
            new Topic { Id = 1, LessonId = 2, Name = "Sözcükte Anlam", OrderIndex = 1, Priority = 3},
            new Topic { Id = 2, LessonId = 2, Name = "Cümlede Anlam", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 3, LessonId = 2, Name = "Sözcük Türleri", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 4, LessonId = 2, Name = "Sözcükte Yapı", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 5, LessonId = 2, Name = "Cümlenin Ögeleri", OrderIndex = 5, Priority = 3 },
            new Topic { Id = 6, LessonId = 2, Name = "Cümle Türleri", OrderIndex = 6, Priority = 3 },
            new Topic { Id = 7, LessonId = 2, Name = "Dil Bilgisi Ses Olayları", OrderIndex = 7, Priority = 3 },
            new Topic { Id = 8, LessonId = 2, Name = "Yazım Kuralları", OrderIndex = 8, Priority = 3 },
            new Topic { Id = 9, LessonId = 2, Name = "Noktalama İşaretleri", OrderIndex = 9, Priority = 3 },
            new Topic { Id = 10, LessonId = 2, Name = "Anlatım Bozuklukları", OrderIndex = 10, Priority = 3 },
            new Topic { Id = 11, LessonId = 2, Name = "Paragrafta Anlam", OrderIndex = 11, Priority = 3 },
            new Topic { Id = 12, LessonId = 2, Name = "Paragrafta Anlatım Biçimi", OrderIndex = 12, Priority = 3 },
            new Topic { Id = 13, LessonId = 2, Name = "Sözel Mantık", OrderIndex = 13, Priority = 3 },
            //matematik
            new Topic { Id = 14, LessonId = 5, Name = "Temel Kavramlar", OrderIndex = 1, Priority = 3 },
            new Topic { Id = 15, LessonId = 5, Name = "Rasyonel Sayılar - Ondalıklı Sayılar", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 16, LessonId = 5, Name = "Basit Eşitsizlikler", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 17, LessonId = 5, Name = "Mutlak Değer", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 18, LessonId = 5, Name = "Üslü Sayılar", OrderIndex = 5, Priority = 3 },
            new Topic { Id = 19, LessonId = 5, Name = "Köklü Sayılar", OrderIndex = 6, Priority = 3 },
            new Topic { Id = 20, LessonId = 5, Name = "Çarpanlara Ayırma", OrderIndex = 7, Priority = 3 },
            new Topic { Id = 21, LessonId = 5, Name = "Oran-Orantı", OrderIndex = 8, Priority = 3 },
            new Topic { Id = 22, LessonId = 5, Name = "Denklem Çözme", OrderIndex = 9, Priority = 3 },
            new Topic { Id = 23, LessonId = 5, Name = "Problemler", OrderIndex = 10, Priority = 3 },
            new Topic { Id = 24, LessonId = 5, Name = "Kümeler", OrderIndex = 11, Priority = 3 },
            new Topic { Id = 25, LessonId = 5, Name = "Fonksiyonlar", OrderIndex = 12, Priority = 3 },
            new Topic { Id = 26, LessonId = 5, Name = "İşlem", OrderIndex = 13, Priority = 3 },
            new Topic { Id = 27, LessonId = 5, Name = "Permütasyon", OrderIndex = 14, Priority = 3 },
            new Topic { Id = 28, LessonId = 5, Name = "Kombinasyon", OrderIndex = 15, Priority = 3 },
            new Topic { Id = 29, LessonId = 5, Name = "Olasılık", OrderIndex = 16, Priority = 3 },
            new Topic { Id = 30, LessonId = 5, Name = "Sayısal Mantık", OrderIndex = 17, Priority = 3 },

            // GEOMETRİ (31-35)
            new Topic { Id = 31, LessonId = 8, Name = "Geometrik Kavramlar ve Açılar", OrderIndex = 1, Priority = 3 },
            new Topic { Id = 32, LessonId = 8, Name = "Çokgenler ve Dörtgenler", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 33, LessonId = 8, Name = "Çember ve Daire", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 34, LessonId = 8, Name = "Analitik Geometri", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 35, LessonId = 8, Name = "Katı Cisimler", OrderIndex = 5, Priority = 3 },

            // TARİH (36-52)
            new Topic { Id = 36, LessonId = 17, Name = "İslamiyet Öncesi Türk Tarihi", OrderIndex = 1, Priority = 3 },
            new Topic { Id = 37, LessonId = 17, Name = "İlk Türk-İslam Devletleri ve Beylikleri", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 38, LessonId = 17, Name = "Osmanlı Devleti Kuruluş ve Yükselme Dönemleri", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 39, LessonId = 17, Name = "Osmanlı Devleti'nde Kültür ve Uygarlık", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 40, LessonId = 17, Name = "XVII. Yüzyılda Osmanlı Devleti (Duraklama)", OrderIndex = 5, Priority = 3 },
            new Topic { Id = 41, LessonId = 17, Name = "XVIII. Yüzyılda Osmanlı Devleti (Gerileme)", OrderIndex = 6, Priority = 3 },
            new Topic { Id = 42, LessonId = 17, Name = "XIX. Yüzyılda Osmanlı Devleti (Dağılma)", OrderIndex = 7, Priority = 3 },
            new Topic { Id = 43, LessonId = 17, Name = "XX. Yüzyılda Osmanlı Devleti", OrderIndex = 8, Priority = 3 },
            new Topic { Id = 44, LessonId = 17, Name = "Kurtuluş Savaşı Hazırlık Dönemi", OrderIndex = 9, Priority = 3 },
            new Topic { Id = 45, LessonId = 17, Name = "I. TBMM Dönemi", OrderIndex = 10, Priority = 3 },
            new Topic { Id = 46, LessonId = 17, Name = "Kurtuluş Savaşı Muharebeler Dönemi", OrderIndex = 11, Priority = 3 },
            new Topic { Id = 47, LessonId = 17, Name = "Atatürk İnkılapları", OrderIndex = 12, Priority = 3 },
            new Topic { Id = 48, LessonId = 17, Name = "Atatürk İlkeleri", OrderIndex = 13, Priority = 3 },
            new Topic { Id = 49, LessonId = 17, Name = "Partiler ve Partileşme Dönemi", OrderIndex = 14 },
            new Topic { Id = 50, LessonId = 17, Name = "Atatürk Dönemi Türk Dış Politikası", OrderIndex = 15, Priority = 3 },
            new Topic { Id = 51, LessonId = 17, Name = "Atatürk Sonrası Dönem", OrderIndex = 16, Priority = 3 },
            new Topic { Id = 52, LessonId = 17, Name = "Atatürk'ün Hayatı ve Kişiliği", OrderIndex = 17, Priority = 3 },

            // COĞRAFYA (53-64)
            new Topic { Id = 53, LessonId = 20, Name = "Türkiye'nin Coğrafi Konumu", OrderIndex = 1, Priority = 3 },
            new Topic { Id = 54, LessonId = 20, Name = "Türkiye'nin İklimi ve Bitki Örtüsü", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 55, LessonId = 20, Name = "Türkiye'nin Fiziki Özellikleri", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 56, LessonId = 20, Name = "Türkiye'de Nüfus ve Yerleşme", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 57, LessonId = 20, Name = "Tarım", OrderIndex = 5, Priority = 3 },
            new Topic { Id = 58, LessonId = 20, Name = "Hayvancılık", OrderIndex = 6, Priority = 3 },
            new Topic { Id = 59, LessonId = 20, Name = "Madenler ve Enerji Kaynakları", OrderIndex = 7, Priority = 3 },
            new Topic { Id = 60, LessonId = 20, Name = "Sanayi ve Endüstri", OrderIndex = 8, Priority = 3 },
            new Topic { Id = 61, LessonId = 20, Name = "Ulaşım", OrderIndex = 9, Priority = 3 },
            new Topic { Id = 62, LessonId = 20, Name = "Ticaret", OrderIndex = 10, Priority = 3 },
            new Topic { Id = 63, LessonId = 20, Name = "Turizm", OrderIndex = 11, Priority = 3 },
            new Topic { Id = 64, LessonId = 20, Name = "Bölgeler Coğrafyası", OrderIndex = 12, Priority = 3 },

            // VATANDAŞLIK (65-72)
            new Topic { Id = 65, LessonId = 26, Name = "Temel Hukuk Kavramları", OrderIndex = 1, Priority = 3 },
            new Topic { Id = 66, LessonId = 26, Name = "Anayasal Kavramlar", OrderIndex = 2, Priority = 3 },
            new Topic { Id = 67, LessonId = 26, Name = "Türk Anayasa Tarihi", OrderIndex = 3, Priority = 3 },
            new Topic { Id = 68, LessonId = 26, Name = "Temel Hak ve Ödevler", OrderIndex = 4, Priority = 3 },
            new Topic { Id = 69, LessonId = 26, Name = "Yasama", OrderIndex = 5, Priority = 3 },
            new Topic { Id = 70, LessonId = 26, Name = "Yürütme", OrderIndex = 6, Priority = 3 },
            new Topic { Id = 71, LessonId = 26, Name = "Yargı", OrderIndex = 7, Priority = 3 },
            new Topic { Id = 72, LessonId = 26, Name = "İdare Hukuku", OrderIndex = 8, Priority = 3 }
            #endregion
        );
    }
}
