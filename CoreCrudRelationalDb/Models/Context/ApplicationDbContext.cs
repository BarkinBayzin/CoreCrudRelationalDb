using CoreCrudRelationalDb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreCrudRelationalDb.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departman>().HasData(
                new Departman() { Id = 1, Ad = "Muhasebe"},
                new Departman() { Id = 2, Ad = "Bilgi İşlem"},
                new Departman() { Id = 3, Ad = "Pazarlama"},
                new Departman() { Id = 4, Ad = "İnsan Kaynakları" },
                new Departman() { Id = 5, Ad = "Planlama ve Üretim" },
                new Departman() { Id = 6, Ad = "Lojistik" },
                new Departman() { Id = 7, Ad = "İdari" }
                );
        }
    }
}
