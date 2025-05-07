
# 📄 Personel Harcama Takip Sistemi - API Dokümantasyonu

## 1. 🔴 Harcama Silme (DELETE)

**Endpoint:**  
`DELETE http://localhost:5068/api/Expense/{id}`

**Açıklama:**  
Belirtilen ID değerine sahip harcama kaydını sistemden silmek için kullanılır.

**Örnek İstek:**

```bash
curl --location --request DELETE 'http://localhost:5068/api/Expense/8' \
--header 'id: 1'
```

**Authorization:**  
Bearer Token gereklidir.

**Header:**  
`id: 1` → İlgili kullanıcı ya da istek kimliği.

**Olası Yanıt (Başarısız):**

```
HTTP 400 Bad Request
Body:
Harcama bulunamadı veya aktif değil
```

**Yanıt Şeması:**

```json
{
  "success": true | false,
  "message": "string | null",
  "serverDate": "string",
  "referenceNo": "string",
  "status": "integer"
}
```

---

## 2. 🔍 Harcama Filtreleme (POST)

**Endpoint:**  
`POST http://localhost:5068/api/Expense/filter`

**Açıklama:**  
Belirtilen kriterlere göre harcamaları filtrelemek için kullanılır.

**Authorization:**  
Bearer Token gereklidir.

**İstek Gövdesi (JSON):**

```json
{
  "paymentCategoryId": 1,
  "demandState": 0,
  "startDate": "2025-05-05T12:38:52.708Z",
  "endDate": "2025-05-06T12:38:52.708Z"
}
```

| Alan Adı           | Tür     | Açıklama                                 |
|--------------------|---------|------------------------------------------|
| paymentCategoryId  | number  | Ödeme kategorisinin ID'si                |
| demandState        | number  | Talep durumu                             |
| startDate          | string  | Başlangıç tarihi (ISO 8601 formatında)   |
| endDate            | string  | Bitiş tarihi (ISO 8601 formatında)       |

**Yanıt Örneği:**

```json
{
  "serverDate": "2025-05-06T12:38:52.708Z",
  "referenceNo": "ABC123456",
  "success": true,
  "status": 0,
  "message": "",
  "response": []
}
```

---

## 3. 🏢 Firma Ödemeleri Raporu (GET)

**Endpoint:**  
`GET http://localhost:5068/api/Reports/CompanyPayment`

**Açıklama:**  
Firma bazlı ödeme raporlarını döner.

**Örnek İstek:**

```bash
curl --location 'http://localhost:5068/api/Reports/CompanyPayment'
```

**Yanıt:**  
Bu istek herhangi bir response body döndürmez.

---

## 4. 🧾 Tüm Personel Harcama Özeti (GET)

**Endpoint:**  
`GET http://localhost:5068/api/Reports/ExpenseSummary`

**Açıklama:**  
Tüm personelin harcama özetlerini döner.

**Örnek İstek:**

```bash
curl --location 'http://localhost:5068/api/Reports/ExpenseSummary' \
--data ''
```

**Yanıt:**  
Bu istek herhangi bir response body döndürmez.

---

## 5. 🔐 Giriş Yapma (POST)

**Endpoint:**  
`POST http://localhost:5068/api/Auth/Token`

**Açıklama:**  
Kullanıcı adı ve şifre ile giriş yapmak için kullanılır. Başarılı girişte JWT Token döner.

**İstek Gövdesi (JSON):**

```json
{
  "userName": "admin",
  "password": ""
}
```
