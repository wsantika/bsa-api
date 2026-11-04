using bsa_api.Data;
using bsa_api.Dtos;
using bsa_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bsa_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KategoriController : ControllerBase
{
    private readonly AppDbContext _db;

    public KategoriController(AppDbContext db) => _db = db;

    // GET api/kategori?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var data = await _db.Kategoris
            .AsNoTracking()
            .OrderBy(k => k.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(k => new KategoriDto
            {
                Id = k.Id,
                Nama = k.Nama,
                WaktuUpdate = k.WaktuUpdate,
                DetailList = k.Produks.Select(p => new ProdukSimpleDto
                {
                    Id = p.Id,
                    Nama = p.Nama,
                    WaktuUpdate = p.WaktuUpdate
                }).ToList()
            })
            .ToListAsync();

        return Ok(ApiResponse<List<KategoriDto>>.Ok(data));
    }

    // GET api/kategori/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _db.Kategoris
            .AsNoTracking()
            .Where(k => k.Id == id)
            .Select(k => new KategoriDto
            {
                Id = k.Id,
                Nama = k.Nama,
                WaktuUpdate = k.WaktuUpdate,
                DetailList = k.Produks.Select(p => new ProdukSimpleDto
                {
                    Id = p.Id,
                    Nama = p.Nama,
                    WaktuUpdate = p.WaktuUpdate
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        // Sesuai spec B2: Data berupa array
        return Ok(ApiResponse<List<KategoriDto>>.Ok(new List<KategoriDto> { item }));
    }

    // POST api/kategori
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] KategoriRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Nama))
            return BadRequest(ApiResponse<object>.Fail("Nama wajib diisi"));

        _db.Kategoris.Add(new Kategori
        {
            Nama = req.Nama.Trim(),
            WaktuUpdate = DateTime.Now
        });
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil disimpan"));
    }

    // PATCH api/kategori/1
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] KategoriRequest req)
    {
        var item = await _db.Kategoris.FindAsync(id);
        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        if (req.Nama != null)
        {
            if (string.IsNullOrWhiteSpace(req.Nama))
                return BadRequest(ApiResponse<object>.Fail("Nama tidak boleh kosong"));
            item.Nama = req.Nama.Trim();
        }

        item.WaktuUpdate = DateTime.Now;
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil disimpan"));
    }

    // DELETE api/kategori/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Kategoris.FindAsync(id);
        if (item == null)
            return NotFound(ApiResponse<object>.Fail("Data tidak ditemukan"));

        _db.Kategoris.Remove(item);
        await _db.SaveChangesAsync();

        return Ok(ApiResponse<object>.Ok(null, "Data berhasil dihapus"));
    }
}