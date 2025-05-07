📘 Personel Masraf Takip API Dokümantasyonu
🔐 Yetkilendirme
Tüm endpointler JWT (Bearer Token) ile korunmaktadır. Yetkili kullanıcı rolleri: Admin, Personnel.

Header'da:

makefile
Authorization: Bearer <token>
# 📂 Masraf Talepleri (ExpenseRequests)
## 📌 1. Masraf Talebi Oluştur
- POST /api/expenserequests

Yetki: Personnel

Body:
json
{
  "title": "Yemek masrafı",
  "amount": 200,
  "categoryId": 1,
  "paymentMethodId": 2,
  "location": "Ankara",
  "expenseDate": "2025-05-07",
  "documentUrl": "https://example.com/fatura.jpg"
}
Response:
json
{
  "id": 1,
  "status": "Pending"
}
## 📌 2. Masraf Taleplerimi Listele
GET /api/expenserequests/my

Yetki: Personnel

Response:
json
[
  {
    "id": 1,
    "title": "Yemek masrafı",
    "amount": 200,
    "status": "Pending",
    "submittedDate": "2025-05-07"
  }
]

## 📌 3. Tüm Masraf Taleplerini Listele (Admin)
GET /api/expenserequests

Yetki: Admin

Query Params:
status (optional): Pending, Approved, Rejected

personnelId (optional)

Response:
json
[
  {
    "id": 1,
    "personnelName": "Ahmet Yılmaz",
    "title": "Yemek masrafı",
    "amount": 200,
    "status": "Pending"
  }
]

## 📌 4. Masraf Talebini Onayla / Reddet
PUT /api/expenserequests/{id}/status

Yetki: Admin

Body:
json
{
  "status": "Approved",
  "adminComment": "Makbuz kontrol edildi, onaylandı."
}

# 📁 Kategoriler (ExpenseCategories)
## 📌 5. Kategorileri Listele
- GET /api/expensecategories

Yetki: Tüm roller

Response:
json
[
  { "id": 1, "name": "Yemek" },
  { "id": 2, "name": "Ulaşım" }
]
## 📌 6. Yeni Kategori Ekle
- POST /api/expensecategories

Yetki: Admin

Body:
json
{
  "name": "Konaklama"
}

# 👤 Kullanıcı Yönetimi
## 📌 8. Giriş Yap

- POST /api/auth/login

Body:
json
{
  "email": "personel@example.com",
  "password": "123456"
}
Response:
json
{
  "token": "<jwt-token>",
  "role": "Personnel"
}