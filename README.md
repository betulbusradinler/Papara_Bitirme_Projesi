# Papara Bootcamp Final Case
# 📊 Saha Personel Gider Takip Sistemi

## 🧾 Proje Tanımı

Bu proje, saha personellerinin yaptığı harcamaların takip edilmesi, onay süreçlerinin yürütülmesi ve yöneticiler tarafından raporlanması amacıyla geliştirilmiştir. ASP.NET Core API, Entity Framework Core, Dapper ve MSSQL kullanılarak geliştirilmiştir. JWT ile güvenli kimlik doğrulama sağlanmıştır.

## 🧩 Özellikler

- 👤 Kullanıcı Rolleri: **Admin** ve **Personel**
- 📝 Gider Bildirim Sistemi
  - Harcama tipi, ödeme yöntemi, konum, belge yükleme (fiş/fatura)
- ✅ Onay / Reddet Süreci (Admin yetkisiyle)
- 📈 Raporlama
  - Günlük, haftalık, aylık gider raporları
  - Kategori bazlı özet
- 🛡️ Güvenli Kimlik Doğrulama (JWT + HMACSHA512 ile şifreleme)
- 🧾 Audit Log Takibi (Kim, ne zaman, ne yaptı bilgileri)
- ❗ Global Exception Middleware ile Hata Yönetimi
- 🔒 ISO Standartlarına Uygun Şifre Güvenliği (Şifreler ayrı tabloda tutulur)

## 🛠️ Kullanılan Teknolojiler

| Teknoloji        | Açıklama                                 |
|------------------|-------------------------------------------|
| ASP.NET Core     | API geliştirme                            |
| EF Core          | Veritabanı işlemleri (Code First)         |
| Dapper           | Performanslı sorgular ve raporlar         |
| PostgreSQL       | Veritabanı                                |
| JWT              | Kimlik doğrulama                          |
| HMACSHA512       | Şifreleme ve güvenlik                     |
| FluentValidation | Girdi doğrulama                           |
| AutoMapper       | DTO-Mapping işlemleri                     |
| Swagger          | API test ve dokümantasyon aracı           |
| MediatR + CQRS   | Sorgu ve komutların ayrıştırılması        |
| Unit of Work     | Transaction yönetimi                      |
| View + SP        | SQL Server View ve Stored Procedure ile raporlama |
| Middleware       | Global hata ve audit log yakalama         |

## 🔐 Rol Bazlı Yetkilendirme

- **Personel**
  - Gider bildirimi yapabilir
  - Kendi giderlerini görebilir
- **Admin**
  - Tüm personellerin giderlerini yönetir
  - Onay / Reddet işlemleri yapar
  - Raporları görüntüler

## 📄 API Kullanımı

Swagger arayüzü üzerinden API'yi test edebilirsiniz:
https://localhost:7180/swagger/index.html
veya 
http://localhost:5068/swagger/index.html

## 🧪 Kurulum ve Çalıştırma

1. Repository'yi klonlayın:

git clone https://github.com/betulbusradinler/Papara_Bitirme_Projesi.git

2. Bağımlılıkları yükleyin ve migration'ı uygulayın:

dotnet restore
dotnet ef migrations add InitialMigration
dotnet ef database update

3. Projeyi başlatın:

dotnet run 
veya 
dotnet watch run
## 🗂️ Klasör Yapısı

ExpenseTracker.Api/
│
├── Controllers/
├── Impl/
│   ├── Command/
│   ├── Query/
│   ├── Validators/
│   ├── Middlewares/
│   └── Logging/
├── Models/
├── DTOs/
├── Repositories/
├── Services/
└── Program.cs

ExpenseTracker.Base/
│
├── ApiResponse/
├── Domain/
│── Session/
├── Models/
└── Token/

ExpenseTracker.Schema/
│
├── Auth
├── Expense
│── ExpenseDetail
└── PaymentCategory

## 📌 Notlar
Projede Code-First yaklaşımı kullanılmıştır. Yeni bir veritabanı oluşturulurken dotnet ef database update komutunun çalıştırılması gerekir.

Swagger üzerinden token alımı için giriş endpoint'ine JWT bilgileri girilmelidir.

## 📥 Gelecek Geliştirmeler
Frontend (React veya Blazor) arayüzü

Harcama limiti tanımı

Excel / PDF rapor çıktıları

Redis ile cache yönetimi

RabbitMQ ile bildirim sistemi

## 🧑‍💻 Geliştirici
Busra Betul Dinler
betulbusradinler@gmail.com
