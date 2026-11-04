namespace bsa_api.Dtos;

public class ProdukSimpleDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public DateTime WaktuUpdate { get; set; }
}

public class KategoriDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public DateTime WaktuUpdate { get; set; }
    public List<ProdukSimpleDto> DetailList { get; set; } = new();
}

public class KategoriRequest
{
    public string? Nama { get; set; }
}