using bsa_api.Data;
using bsa_api.Dtos;
using bsa_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bsa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdukController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdukController(AppDbContext db) => _db = db;

    // GET api/produk?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var data = await _db.Produks
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProdukDto
            {
                Id = p.Id,
                Nama = p.Nama,
                IdParent = p.IdKategori,
                NamaParent = p.Kategori.Nama,
                WaktuUpdate = p.WaktuUpdate
            })
            .ToListAsync();

        return Ok(ApiResponse<List<ProdukDto>>.Ok(data));
    }

    // GET api/produk/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _db.Produks
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProdukDto
            {
                Id = p.Id,
                Nama = p.Nama,
                IdParent = p.IdKategori,
                NamaParent = p.Kategori.Nama,
                WaktuUpdate = p.WaktuUpdate
            })
            .FirstOrDefaultAsync();

        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        return Ok(ApiResponse<ProdukDto>.Ok(item));
    }

    // POST api/produk
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProdukRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Nama))
            return BadRequest(ApiResponse<object>.Fail("Nama wajib diisi"));

        if (req.IdKategori == null)
            return BadRequest(ApiResponse<object>.Fail("IdKategori wajib diisi"));

        var kategoriExists = await _db.Kategoris.AnyAsync(k => k.Id == req.IdKategori.Value);
        if (!kategoriExists)
            return NotFound(ApiResponse<object>.Fail("Kategori tidak ditemukan"));

        _db.Produks.Add(new Produk
        {
            Nama = req.Nama.Trim(),
            IdKategori = req.IdKategori.Value,
            WaktuUpdate = DateTime.Now
        });
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil disimpan"));
    }

    // PATCH api/produk/1
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdukRequest req)
    {
        var item = await _db.Produks.FindAsync(id);
        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        if (req.Nama != null)
        {
            if (string.IsNullOrWhiteSpace(req.Nama))
                return BadRequest(ApiResponse<object>.Fail("Nama tidak boleh kosong"));
            item.Nama = req.Nama.Trim();
        }

        if (req.IdKategori != null)
        {
            var kategoriExists = await _db.Kategoris.AnyAsync(k => k.Id == req.IdKategori.Value);
            if (!kategoriExists)
                return NotFound(ApiResponse<object>.Fail("Kategori tidak ditemukan"));
            item.IdKategori = req.IdKategori.Value;
        }

        item.WaktuUpdate = DateTime.Now;
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil disimpan"));
    }

    // DELETE api/produk/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Produks.FindAsync(id);
        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        _db.Produks.Remove(item);
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil dihapus"));
    }
}
