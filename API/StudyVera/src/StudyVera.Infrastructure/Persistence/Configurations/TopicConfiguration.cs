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
        // MATEMATİK (LessonId = 2)
        new Topic { Id = 1, LessonId = 2, Name = "İşlem Yeteneği", OrderIndex = 1, Priority = 3 },
        new Topic { Id = 2, LessonId = 2, Name = "Temel Kavramlar", OrderIndex = 2, Priority = 3 },
        new Topic { Id = 3, LessonId = 2, Name = "Tek - Çift - Pozitif - Negatif Sayılar", OrderIndex = 3, Priority = 3 },
        new Topic { Id = 4, LessonId = 2, Name = "Ardışık Sayılar", OrderIndex = 4, Priority = 3 },
        new Topic { Id = 5, LessonId = 2, Name = "Faktöriyel", OrderIndex = 5, Priority = 3 },
        new Topic { Id = 6, LessonId = 2, Name = "Sayı Basamakları ve Taban Aritmetiği", OrderIndex = 6, Priority = 3 },
        new Topic { Id = 7, LessonId = 2, Name = "Bölme - Bölünebilme", OrderIndex = 7, Priority = 3 },
        new Topic { Id = 8, LessonId = 2, Name = "Asal Çarpanlara Ayırma", OrderIndex = 8, Priority = 3 },
        new Topic { Id = 9, LessonId = 2, Name = "EBOB - EKOK", OrderIndex = 9, Priority = 3 },
        new Topic { Id = 10, LessonId = 2, Name = "Rasyonel Sayılar", OrderIndex = 10, Priority = 3 },
        new Topic { Id = 11, LessonId = 2, Name = "Basit Eşitsizlikler", OrderIndex = 11, Priority = 3 },
        new Topic { Id = 12, LessonId = 2, Name = "Mutlak Değer", OrderIndex = 12, Priority = 3 },
        new Topic { Id = 13, LessonId = 2, Name = "Üslü Sayılar", OrderIndex = 13, Priority = 3 },
        new Topic { Id = 14, LessonId = 2, Name = "Köklü Sayılar", OrderIndex = 14, Priority = 3 },
        new Topic { Id = 15, LessonId = 2, Name = "Çarpanlara Ayırma", OrderIndex = 15, Priority = 3 },
        new Topic { Id = 16, LessonId = 2, Name = "Oran - Orantı", OrderIndex = 16, Priority = 3 },
        new Topic { Id = 17, LessonId = 2, Name = "Birinci Dereceden Denklemler", OrderIndex = 17, Priority = 3 },
        new Topic { Id = 18, LessonId = 2, Name = "Sayı Problemleri", OrderIndex = 18, Priority = 3 },
        new Topic { Id = 19, LessonId = 2, Name = "Kesir Problemleri", OrderIndex = 19, Priority = 3 },
        new Topic { Id = 20, LessonId = 2, Name = "Yaş Problemleri", OrderIndex = 20, Priority = 3 },
        new Topic { Id = 21, LessonId = 2, Name = "Hareket Problemleri", OrderIndex = 21, Priority = 3 },
        new Topic { Id = 22, LessonId = 2, Name = "İşçi - Havuz Problemleri", OrderIndex = 22, Priority = 3 },
        new Topic { Id = 23, LessonId = 2, Name = "Yüzde - Kâr - Zarar - Faiz Problemleri", OrderIndex = 23, Priority = 3 },
        new Topic { Id = 24, LessonId = 2, Name = "Karışım Problemleri", OrderIndex = 24, Priority = 3 },
        new Topic { Id = 25, LessonId = 2, Name = "Grafik Problemleri", OrderIndex = 25, Priority = 3 },
        new Topic { Id = 26, LessonId = 2, Name = "Kümeler", OrderIndex = 26, Priority = 3 },
        new Topic { Id = 27, LessonId = 2, Name = "İşlem - Modüler Aritmetik", OrderIndex = 27, Priority = 3 },
        new Topic { Id = 28, LessonId = 2, Name = "Permütasyon - Kombinasyon - Olasılık", OrderIndex = 28, Priority = 3 },
        new Topic { Id = 29, LessonId = 2, Name = "Fonksiyonlar", OrderIndex = 29, Priority = 3 },
        new Topic { Id = 30, LessonId = 2, Name = "Sayısal Mantık", OrderIndex = 30, Priority = 3 },

        // GEOMETRİ (LessonId = 3)
        new Topic { Id = 31, LessonId = 3, Name = "Üçgenler", OrderIndex = 1, Priority = 3 },
        new Topic { Id = 32, LessonId = 3, Name = "Çokgenler - Dörtgenler", OrderIndex = 2, Priority = 3 },
        new Topic { Id = 33, LessonId = 3, Name = "Çember - Daire", OrderIndex = 3, Priority = 3 },
        new Topic { Id = 34, LessonId = 3, Name = "Katı Cisimler", OrderIndex = 4, Priority = 3 },
        new Topic { Id = 35, LessonId = 3, Name = "Analitik Geometri", OrderIndex = 5, Priority = 3 },

        // TARİH (LessonId = 4)
        new Topic { Id = 36, LessonId = 4, Name = "İslamiyet Öncesi Türk Tarihi", OrderIndex = 1, Priority = 3 },
        new Topic { Id = 37, LessonId = 4, Name = "İlk Türk - İslam Devletleri", OrderIndex = 2, Priority = 3 },
        new Topic { Id = 38, LessonId = 4, Name = "Anadolu (Türkiye) Selçuklu Devleti", OrderIndex = 3, Priority = 3 },
        new Topic { Id = 39, LessonId = 4, Name = "Osmanlı Devleti Kültür ve Medeniyeti", OrderIndex = 4, Priority = 3 },
        new Topic { Id = 40, LessonId = 4, Name = "Osmanlı Devleti Kuruluş Dönemi (1299 - 1453)", OrderIndex = 5, Priority = 3 },
        new Topic { Id = 41, LessonId = 4, Name = "Osmanlı Devleti Yükselme Dönemi (1453 - 1595)", OrderIndex = 6, Priority = 3 },
        new Topic { Id = 42, LessonId = 4, Name = "XVII. Yüzyılda Osmanlı Devleti (Duraklama Dönemi) (1595 - 1699)", OrderIndex = 7, Priority = 3 },
        new Topic { Id = 43, LessonId = 4, Name = "XVIII. Yüzyılda Osmanlı Devleti (Gerileme Dönemi) (1699 - 1792)", OrderIndex = 8, Priority = 3 },
        new Topic { Id = 44, LessonId = 4, Name = "XIX. Yüzyılda Osmanlı Devleti (Dağılma Dönemi) (1792 - 1922)", OrderIndex = 9, Priority = 3 },
        new Topic { Id = 45, LessonId = 4, Name = "XX. Yüzyıl Başlarında Osmanlı Devleti", OrderIndex = 10, Priority = 3 },
        new Topic { Id = 46, LessonId = 4, Name = "Mondros Ateşkes Antlaşması ve İlk İşgaller", OrderIndex = 11, Priority = 3 },
        new Topic { Id = 47, LessonId = 4, Name = "Milli Mücadele Hazırlık Dönemi", OrderIndex = 12, Priority = 3 },
        new Topic { Id = 48, LessonId = 4, Name = "I. TBMM Dönemi ve Gelişmeleri (1920 - 1923)", OrderIndex = 13, Priority = 3 },
        new Topic { Id = 49, LessonId = 4, Name = "Milli Mücadele Muharebeler Dönemi", OrderIndex = 14, Priority = 3 },
        new Topic { Id = 50, LessonId = 4, Name = "Atatürk'ün Hayatı", OrderIndex = 15, Priority = 3 },
        new Topic { Id = 51, LessonId = 4, Name = "Atatürk Dönemi İç Politika", OrderIndex = 16, Priority = 3 },
        new Topic { Id = 52, LessonId = 4, Name = "Atatürk İlkeleri", OrderIndex = 17, Priority = 3 },
        new Topic { Id = 53, LessonId = 4, Name = "Atatürk İnkılapları", OrderIndex = 18, Priority = 3 },
        new Topic { Id = 54, LessonId = 4, Name = "Atatürk Dönemi Türk Dış Politikası", OrderIndex = 19, Priority = 3 },
        new Topic { Id = 55, LessonId = 4, Name = "Cumhuriyet Dönemi Kültür ve Medeniyeti", OrderIndex = 20, Priority = 3 },
        new Topic { Id = 56, LessonId = 4, Name = "XX. Yüzyıl Başlarında Dünya (1918 - 1939)", OrderIndex = 21, Priority = 3 },
        new Topic { Id = 57, LessonId = 4, Name = "II. Dünya Savaşı (1939 - 1945)", OrderIndex = 22, Priority = 3 },
        new Topic { Id = 58, LessonId = 4, Name = "Soğuk Savaş Dönemi (1947 - 1990)", OrderIndex = 23, Priority = 3 },
        new Topic { Id = 59, LessonId = 4, Name = "Yumuşama Dönemi (1961 - 1990)", OrderIndex = 24, Priority = 3 },
        new Topic { Id = 60, LessonId = 4, Name = "Küreselleşen Dünya (1990 - 2026)", OrderIndex = 25, Priority = 3 }
        // COĞRAFYA (53-64)

        // VATANDAŞLIK (65-72)
        #endregion
        );
    }
}
