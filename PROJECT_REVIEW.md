# StudyVera - Proje İnceleme Raporu

Bu rapor, projenin kaynak kodları, mimarisi, dizin yapısı ve yapılandırma dosyalarının derinlemesine analiz edilmesi sonucunda hazırlanmıştır.

## 1. Hata Analizi (Runtime & Mantık Hataları)

- **Loglama Olmayan Exception Middleware (Potansiyel Kayıp Hatalar):**
  `StudyVera.WebApi/Middleware/ExceptionMiddleware.cs` içinde yakalanan hatalar `StatusCodes.Status500InternalServerError` olarak dönülüyor ancak herhangi bir `ILogger` kullanılarak loglanmıyor. Bu durum, canlı ortamda (production) uygulamanın neden crash olduğunu anlamayı imkansızlaştırır.
  *Gerekçe:* `appError.Run(async context => { ... })` bloğu içerisinde loglama işlemi eksik.

- **Kayıt ve Validasyon Hatalarının 500 Dönmesi:**
  `RegisterHandler.cs` (ve potansiyel olarak diğer handler'lar) işlem başarısız olduğunda `ValidationException` fırlatıyor. Ancak `ExceptionMiddleware.cs` bu exception tipini bilmediği için 400 Bad Request yerine 500 Internal Server Error dönüyor.
  *Gerekçe:* Client'a nerede hata yaptığını söylemek yerine uygulamanın çöktüğünü hissettiriyor.

## 2. Kod Kalitesi ve Refactoring

- **Uygulama Katmanında Merkezi Doğrulama (Validation) Eksikliği:**
  Clean Architecture'da MediatR kullanılıyorsa, FluentValidation ve MediatR Pipeline Behavior ikilisi standarttır. Projede `StudyVera.Application` katmanında komutlar (Command) için yapılandırılmış bir FluentValidation pipeline'ı bulunmuyor.
  *Öneri:* Uygulama genelinde gelen her DTO/Command için bir `IPipelineBehavior` yazılıp validasyon işlemleri otomatikleştirilmelidir.

- **Hard-Coded Parametreler ve Magic Number'lar:**
  README dosyasında da belirtilen Eksik Konu Tespit Algoritmasında (Örn: `p = 0.60f`, `minGuven = 10` vb.) magic number'lar mevcut. Bu ayarların veritabanından veya `appsettings.json` üzerinden okunması, algoritmanın ileride dinamik olarak güncellenmesine olanak tanır.

## 3. Güvenlik ve Performans

- **Büyük Tablolarda İndeks (Index) Eksikliği:**
  `AppDbContext.cs` incelendiğinde; `UserActivityHistory`, `UserLessonProgress`, ve `UserQuestionStat` gibi zamanla devasa boyutlara ulaşacak olan transaksiyonel tablolarda `UserId` gibi sık sorgulanan Foreign Key'ler üzerinde özel indeks (Index) yapılandırması görülmemektedir. Bu, veritabanında Full Table Scan yapılmasına ve ciddi performans darboğazlarına (bottleneck) yol açacaktır.

- **Over-fetching (Aşırı Veri Çekme) Problemi:**
  `UserActivityHistoryRepository` içindeki `GetAllByUserAsycn` metodu gibi bazı okuma işlemleri, veritabanından kullanıcıya ait *tüm* geçmişi pagination (sayfalama) yapmadan `ToListAsync()` ile çekmektedir. Aktif bir öğrencide on binlerce aktivite olabileceği düşünüldüğünde bu durum yüksek bellek tüketimine sebep olur.
  *Öneri:* `Skip()` ve `Take()` ile sayfalama yapılmalıdır.

## 4. Yeni Özellik Önerileri

Projenin vizyonunu genişletecek 5 yenilikçi özellik:

1. **Yapay Zeka Destekli Nokta Atışı Denemeler:** Eksik Konu Algoritması'ndan çıkan veri ile OpenAI vb. bir API kullanılarak, öğrencinin en zayıf olduğu konuları birleştiren *kişiye özel hibrit soru bankası/deneme* oluşturulması.
2. **Forgetting Curve (Unutma Eğrisi) Bildirimleri:** Öğrencinin başarılı olduğu ancak üzerinden zaman geçen konuları unutmasını engellemek için, Ebbinghaus Unutma Eğrisi'ne göre hesaplanmış optimum günlerde (1., 3., 7. gün) push bildirim veya SMS ile hatırlatmalar yapılması.
3. **Pomodoro ve Lofi-Odak Modu:** `StudySession` mekanizmasının içine gömülü bir Pomodoro sayacı ve doğrudan uygulama içerisinden dinlenebilecek odak artırıcı (Lofi/White Noise) müzik entegrasyonu.
4. **Rekabetçi Lig ve Rozet Sistemi (Gamification):** Mevcut arkadaşlık (Friendship) sisteminin genişletilerek haftalık `UserWeeklyGoal` başarılarına göre öğrencilerin Demir, Bronz, Gümüş gibi liglere yerleştirilmesi ve dijital rozetler verilmesi.
5. **Koçluk/Rehberlik Dashboard'u:** Veliler veya eğitim koçları için öğrencinin performans grafiğini (ProfileStat, RankResults) dışarıya PDF/Excel formatında aktarabilen ve salt okunur izleme sunan özel bir portal.

## 5. Teknoloji Borcu (Tech Debt)

- **Kritik: Çok Eski Identity Paketlerinin Kullanımı:**
  Proje modern `.NET 9 Core` altyapısı ile tasarlanmış (Örn: `WebApi` tarafı `9.0.x` kullanıyor). Ancak `StudyVera.Application.csproj` dosyasında `Microsoft.AspNetCore.Identity (v2.3.1)` ve `Microsoft.AspNetCore.Mvc.Core (v2.3.0)` gibi oldukça eski sürümler referans alınmış. Bu, güvenlik zafiyetlerine ve ileride dependency çakışmalarına yol açacak ciddi bir teknik borçtur.

- **Yanıltıcı Dokümantasyon (Blazor vs Angular):**
  Ana `README.md` dosyasında projenin frontend mimarisinin **Blazor** kullanılarak geliştirildiği belirtilmektedir. Ancak `Frontend/StudyVera` dizini incelendiğinde uygulamanın modern bir **Angular (v21)** projesi olduğu görülmektedir. README'nin ve projenin diğer mimari dokümanlarının acilen mevcut gerçekliğe göre güncellenmesi gerekmektedir.
