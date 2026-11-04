# 📖 Dokumentasi Lengkap Endpoint REST API

Dokumentasi seluruh endpoint API untuk entitas **Kategori (Parent)** dan **Produk (Child)** sesuai spesifikasi pada `Soal BSA.pdf`.

- **Base URL:** `http://localhost:5104` atau `https://localhost:7294`
- **Interactive Swagger UI:** `http://localhost:5104/swagger`
- **Format Data:** `application/json`

---

## 📌 Standar Format Response

Semua endpoint mengembalikan JSON dengan struktur pembungkus (*envelope*):

### 1. Response Sukses (Data List / Single)
```json
{
  "Status": true,
  "Message": "Success",
  "Data": ...
}
```

### 2. Response Operasi (POST, PATCH, DELETE)
```json
{
  "Status": true,
  "Message": "Data berhasil disimpan",
  "Data": null
}
```

### 3. Response Error (400 Bad Request / 404 Not Found)
```json
{
  "Status": false,
  "Message": "Data tidak ditemukan",
  "Data": null
}
```

---

## 🏷️ Kelompok Endpoint: Kategori (Parent)

### 1. GET `/api/Kategori`
Menampilkan seluruh data kategori dengan pagination beserta seluruh data produk yang berelasi (*DetailList*).

- **Query Parameters:**
  - `page` (integer, default: 1): Nomor halaman.
  - `pageSize` (integer, default: 10, max: 100): Jumlah data per halaman.
- **Contoh Request:** `GET /api/Kategori?page=1&pageSize=10`
- **Response Sesuai Spec A1 (200 OK):**
```json
{
  "Status": true,
  "Message": "Success",
  "Data": [
    {
      "Id": 1,
      "Nama": "Elektronik",
      "WaktuUpdate": "2026-11-03T22:20:12",
      "DetailList": [
        {
          "Id": 1,
          "Nama": "Laptop",
          "WaktuUpdate": "2026-11-03T22:20:12"
        },
        {
          "Id": 2,
          "Nama": "Smartphone",
          "WaktuUpdate": "2026-11-03T22:20:12"
        }
      ]
    }
  ]
}
```

---

### 2. GET `/api/Kategori/{id}`
Menampilkan single data kategori berdasarkan ID beserta seluruh produk yang berelasi.

- **Path Parameter:**
  - `id` (integer, required): ID Kategori.
- **Contoh Request:** `GET /api/Kategori/1`
- **Response Sesuai Spec B2 (200 OK - Array):**
```json
{
  "Status": true,
  "Message": "Success",
  "Data": [
    {
      "Id": 1,
      "Nama": "Elektronik",
      "WaktuUpdate": "2026-11-03T22:20:12",
      "DetailList": [
        {
          "Id": 1,
          "Nama": "Laptop",
          "WaktuUpdate": "2026-11-03T22:20:12"
        }
      ]
    }
  ]
}
```
- **Response Error (404 Not Found):**
```json
{
  "Status": false,
  "Message": "Data tidak ditemukan",
  "Data": null
}
```

---

### 3. POST `/api/Kategori`
Menambahkan data kategori baru.

- **Request Body (JSON):**
```json
{
  "Nama": "Buku & Alat Tulis"
}
```
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil disimpan",
  "Data": null
}
```
- **Response Error (400 Bad Request):**
```json
{
  "Status": false,
  "Message": "Nama wajib diisi",
  "Data": null
}
```

---

### 4. PATCH `/api/Kategori/{id}`
Mengubah data kategori yang sudah ada.

- **Path Parameter:** `id` (integer, required)
- **Request Body (JSON):**
```json
{
  "Nama": "Alat Tulis Kantor"
}
```
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil disimpan",
  "Data": null
}
```

---

### 5. DELETE `/api/Kategori/{id}`
Menghapus data kategori beserta seluruh produk yang berelasi (*Cascade Delete*).

- **Path Parameter:** `id` (integer, required)
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil dihapus",
  "Data": null
}
```

---

## 📦 Kelompok Endpoint: Produk (Child)

### 6. GET `/api/Produk`
Menampilkan seluruh data produk dengan pagination beserta informasi kategori induknya (*IdParent* & *NamaParent*).

- **Query Parameters:**
  - `page` (integer, default: 1): Nomor halaman.
  - `pageSize` (integer, default: 10, max: 100): Jumlah data per halaman.
- **Contoh Request:** `GET /api/Produk?page=1&pageSize=10`
- **Response Sesuai Spec A2 (200 OK):**
```json
{
  "Status": true,
  "Message": "Success",
  "Data": [
    {
      "Id": 1,
      "Nama": "Laptop",
      "IdParent": 1,
      "NamaParent": "Elektronik",
      "WaktuUpdate": "2026-11-03T22:20:12"
    },
    {
      "Id": 2,
      "Nama": "Smartphone",
      "IdParent": 1,
      "NamaParent": "Elektronik",
      "WaktuUpdate": "2026-11-03T22:20:12"
    }
  ]
}
```

---

### 7. GET `/api/Produk/{id}`
Menampilkan single data produk berdasarkan ID beserta informasi kategori induk.

- **Path Parameter:**
  - `id` (integer, required): ID Produk.
- **Contoh Request:** `GET /api/Produk/1`
- **Response Sesuai Spec B1 (200 OK - Single Object):**
```json
{
  "Status": true,
  "Message": "Success",
  "Data": {
    "Id": 1,
    "Nama": "Laptop",
    "IdParent": 1,
    "NamaParent": "Elektronik",
    "WaktuUpdate": "2026-11-03T22:20:12"
  }
}
```

---

### 8. POST `/api/Produk`
Menambahkan data produk baru dan menghubungkannya dengan kategori induk.

- **Request Body (JSON):**
```json
{
  "Nama": "Mouse Wireless",
  "IdKategori": 1
}
```
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil disimpan",
  "Data": null
}
```

---

### 9. PATCH `/api/Produk/{id}`
Mengubah data produk (nama produk atau kategori induknya).

- **Path Parameter:** `id` (integer, required)
- **Request Body (JSON):**
```json
{
  "Nama": "Mouse Gaming RGB",
  "IdKategori": 1
}
```
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil disimpan",
  "Data": null
}
```

---

### 10. DELETE `/api/Produk/{id}`
Menghapus data produk berdasarkan ID.

- **Path Parameter:** `id` (integer, required)
- **Response Sesuai Spec C (200 OK):**
```json
{
  "Status": true,
  "Message": "Data berhasil dihapus",
  "Data": null
}
```
