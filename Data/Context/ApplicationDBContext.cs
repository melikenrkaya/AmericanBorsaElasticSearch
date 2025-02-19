using AmericanBorsaElasticSearch.Data.Entity;
using Microsoft.EntityFrameworkCore;


namespace AmericanBorsaElasticSearch.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<HisseSenedi> HisseSenedii { get; set; } // Ürünler tablosu

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
