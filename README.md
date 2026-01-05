# 🎓 StudyVera - Akıllı Sınav Takip Sistemi

StudyVera, sınav sürecindeki öğrencilerin çalışma performansını veriye dayalı olarak analiz eden ve kişiye özel programlar sunan **tamamen ücretsiz** bir platformdur.

[**StudyVera'yı Canlıda Gör**](https://studyvera.tech)

---

## 🎯 Projenin Amacı

Sınav hazırlık sürecinde en kritik konu, hangi alanda eksik olduğunuzu doğru tespit etmektir. StudyVera:
- Çözülen soruları konu bazlı takip eder.
- Başarı oranını ve zaman faktörünü (unutma eğrisi) hesaplayarak eksik konuları belirler.
- Bu verilere dayanarak kişiye özel haftalık çalışma programı hazırlar.

> **Neden Ücretsiz?** Kendi sınav dönemimde benzer bir sistemi kendim için geliştirip büyük fayda gördüm. Bu faydayı herkesin erişimine sunmak istedim.

---

## 🏗️ Mimari Yapı: Clean Architecture

Proje, geleneksel n-tier mimarilerdeki bağımlılık (dependency) problemlerini aşmak ve daha sürdürülebilir bir yapı kurmak amacıyla **Clean Architecture** prensiplerine göre tasarlanmıştır.(n-tier architecture ile fazlasıyla proje geliştirdim... yorucu)

- **Frontend:** Blazor
- **Backend:** .NET 9 Core
- **Neden Clean Architecture?** Bağımlılık yönetiminin daha esnek olması ve geliştirme hızını (popülerliği ve deneyimleme isteğiyle birleşince) artırması.

📖 **Mimarideki şahsi fikirlerim ve notlarım:** [Clean Architecture Notları](notes/cleanarchitecture.md)

---

## 🧠 Eksik Konu Tespit Algoritması

Sistem, bir konunun "eksiklik puanını" sadece doğru/yanlış sayısına göre değil; **zamanın etkisi**, **güven aralığı** ve **konu önceliği** gibi parametrelerle hesaplar.

```csharp
private float intCalculateEksik(int TotalSolvedCount, int CorrectCount, DateTime AttemptedAt, int topicId)
{
    int beforeDays = (DateTime.Now - AttemptedAt).Days;
    float p = 0.60f; // Beklenen taban başarı
    int minGuven = 10; // İstatistiksel güven eşiği
    float priorty = Topics.Where(t => t.Id == topicId).Select(t => t.Priority).FirstOrDefault();
    
    float w1 = 0.7f; // Başarı ağırlığı
    float w2 = 0.3f; // Güncellik ağırlığı
    int GuncellikSiniri = 90;

    float GuncellikPuanı = Math.Min(beforeDays, GuncellikSiniri);
    float ABY_Ratio = (((float)CorrectCount + minGuven * p) / ((float)TotalSolvedCount + minGuven));
    float ABY_Percent = ABY_Ratio * 100f;
    
    float BasariKayipPuanı = 100f - ABY_Percent;
    float EksikPuanı = (w1 * BasariKayipPuanı) + (w2 * GuncellikPuanı);
    
    return EksikPuanı * priorty;
}
```
## 🧠 API Routes : 

