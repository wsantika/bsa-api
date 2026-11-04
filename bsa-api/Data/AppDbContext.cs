using bsa_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bsa_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Kategori> Kategoris => Set<Kategori>();
    public DbSet<Produk> Produks => Set<Produk>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategori>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nama).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Produk>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nama).IsRequired().HasMaxLength(100);
            e.HasOne(x => x.Kategori)
             .WithMany(k => k.Produks)
             .HasForeignKey(x => x.IdKategori)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}