# AmericanBorsaElasticSearch

Bu proje, Amerikan borsasında yer alan hisselerin ElasticSearch ile indekslenmesini ve sorgulanmasını sağlayan, .NET Core tabanlı bir web API uygulamasıdır.

---

## 🚀 Kullanılan Teknolojiler

* **ASP.NET Core 8.0**
* **Entity Framework Core** (Code First - SQL Server)
* **ElasticSearch** (Arama altyapısı)
* **Swagger UI** (API test ve dökümantasyon)

---

## 🔧 Özellikler

* Amerikan borsasından hisse verisi kazıma (Scraping)
* Scraper ile elde edilen hisseleri veritabanına kaydetme
* ElasticSearch'e senkron indeksleme
* Hisse senedi adına göre arama yapabilme (full-text)
* DTO ve Entity ayrımı ile temiz veri modeli

---

## 📁 Proje Yapısı

```
AmericanBorsaElasticSearch/
├── Controller/               # BorsaController
├── Data/Context              # DBContext
├── Data/Entity               # HisseSenedi Entity
├── Services/ElasticSearch    # ElasticServi.cs
├── Services/ScraperServi     # BorsaServi.cs (veri kazıyıcı)
├── Pages/                    # Razor Pages
├── Program.cs
├── appsettings.json
```

---

## 📆 Kurulum Adımları

1. ElasticSearch sunucusunu lokal veya uzak olarak ayağa kaldır.
2. `appsettings.json` dosyasından Elastic URI ayarını yap.
3. Bağımlılıkları yükle:

```bash
dotnet restore
```

4. Migration'ları uygula:

```bash
dotnet ef database update
```

5. Uygulamaya başla:

```bash
dotnet run
```

6. Swagger arayüzüne git:

```
https://localhost:{PORT}/swagger
```

---

## 🔍 ElasticSearch Entegrasyonu

* `ElasticServi.cs` içerisinde indexleme ve sorgulama mantığı vardır.
* Endpoint: `GET /Borsa/Search?query=AAPL`
* Tüm veriler scraping sonrası hem DB'ye hem de Elastic'e gider.

---

## 🚧 Veri Kazıma (Scraping)

* `BorsaServi.cs` içinde yer alan scraper logic ile borsa verileri çekilir.
* Bu veriler hem kaydedilir hem indekslenir.

---

## 😍 Geliştirici Notları

* Kodlar SOLID prensiplerine uygun yazılmıştır.
* Scraper + ElasticSearch + DB entegrasyonu ile tam senkron mimari sağlanır.

---

## 📅 Katkıda Bulunmak

Pull request ve katkılarınız memnuniyetle kabul edilir.

---

## 👤 Geliştirici

Bu proje, .NET ile borsa scraping ve ElasticSearch deneyimi kazanmak amacıyla **Melikenur Kaya** tarafından geliştirilmiştir.
