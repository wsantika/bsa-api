namespace bsa_api.Models;

public class Kategori
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public DateTime WaktuUpdate { get; set; }

    public List<Produk> Produks { get; set; } = new();
}