namespace bsa_api.Dtos;

public class ProdukDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public int IdParent { get; set; }
    public string NamaParent { get; set; } = string.Empty;
    public DateTime WaktuUpdate { get; set; }
}

public class ProdukRequest
{
    public string? Nama { get; set; }
    public int? IdKategori { get; set; }
}