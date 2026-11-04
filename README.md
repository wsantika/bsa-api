# BSA API - Kategori & Produk

REST API ASP.NET Core Web API (.NET 8) dengan MySQL dan Entity Framework Core.
Relasi One-to-Many: Kategori (Parent) -> Produk (Child).

## Konfigurasi Lingkungan (.env)
Proyek ini menggunakan variabel lingkungan (`.env`) untuk menjaga keamanan kredensial database (tidak di-hardcode ke file konfigurasi atau repositori).

1. Salin template `.env.example` menjadi `.env`:
   ```bash
   cp .env.example .env
   ```
2. Sesuaikan nilai di dalam `.env` bila konfigurasi MySQL Anda berbeda:
   ```env
   DB_HOST=localhost
   DB_PORT=3306
   DB_DATABASE=bsa_db
   DB_USERNAME=root
   DB_PASSWORD=
   ASPNETCORE_ENVIRONMENT=Development
   ```

## Cara Menjalankan
1. Pastikan service MySQL aktif (misal via Laragon atau XAMPP).
2. Buat database dan import data dummy menggunakan script [database.sql](database.sql):
   ```bash
   mysql -u root < database.sql
   ```
3. Jalankan aplikasi:
   ```bash
   dotnet run
   ```
4. Buka Swagger UI di browser:
   - `http://localhost:5104/swagger` atau `https://localhost:7294/swagger`

## Dokumentasi Endpoint
Dokumentasi lengkap mengenai request header, body, query param, serta contoh payload respon JSON untuk seluruh endpoint dapat dilihat pada:
- **[docs/ENDPOINTS.md](docs/ENDPOINTS.md)** (Dokumentasi Detail & Skema Respon JSON)
- **[bsa-api.http](bsa-api.http)** (Kumpulan Request Siap Uji untuk Visual Studio / VS Code REST Client)
- **Swagger UI:** Akses langsung di `http://localhost:5104/swagger` saat aplikasi dijalankan.

### Ringkasan Endpoint Kategori (Parent)
| Method | URL | Keterangan |
|--------|-----|------------|
| GET | /api/Kategori?page=1&pageSize=10 | List + DetailList produk (pagination) |
| GET | /api/Kategori/{id} | Detail + DetailList produk |
| POST | /api/Kategori | Tambah. Body: `{ "Nama": "Buku" }` |
| PATCH | /api/Kategori/{id} | Ubah. Body: `{ "Nama": "Alat Tulis" }` |
| DELETE | /api/Kategori/{id} | Hapus (produk terkait ikut terhapus) |

## Endpoint Produk
| Method | URL | Keterangan |
|--------|-----|------------|
| GET | /api/Produk?page=1&pageSize=10 | List + IdParent, NamaParent (pagination) |
| GET | /api/Produk/{id} | Detail + IdParent, NamaParent |
| POST | /api/Produk | Tambah. Body: `{ "Nama": "Mouse", "IdKategori": 1 }` |
| PATCH | /api/Produk/{id} | Ubah. Body: `{ "Nama": "Mouse Gaming" }` |
| DELETE | /api/Produk/{id} | Hapus |

## Format Response
- Sukses: `{ "Status": true, "Message": "Success", "Data": ... }`
- POST/PATCH/DELETE: `{ "Status": true, "Message": "Data berhasil disimpan" / "Data berhasil dihapus", "Data": null }`
- Error: `{ "Status": false, "Message": "Data tidak ditemukan", "Data": null }`

## Screenshot Pengujian
Seluruh screenshot pengujian endpoint (10 endpoint sukses + 2 kasus error) tersimpan pada folder `screenshots/`.
