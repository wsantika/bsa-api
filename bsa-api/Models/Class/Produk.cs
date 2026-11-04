namespace bsa_api.Models;

public class Produk
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public DateTime WaktuUpdate { get; set; }

    public int IdKategori { get; set; }
    public Kategori Kategori { get; set; } = null!;
}