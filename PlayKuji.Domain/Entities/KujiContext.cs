using Microsoft.EntityFrameworkCore;

namespace PlayKuji.Domain.Entities
{
    public class KujiContext : DbContext
    {
        public KujiContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductAward> ProductAward { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ID).IsClustered(false);
                entity.Property(x => x.SequenceNo).UseIdentityColumn().ValueGeneratedOnAdd();
                entity.Property(x => x.SaleDate).HasColumnType("date");
                entity.Property(x => x.Price).HasColumnType("decimal(18, 0)");
                entity.Property(x => x.CreateTime).HasColumnType("datetime2(7)");
                entity.Property(x => x.UpdateTime).HasColumnType("datetime2(7)");
            });

            modelBuilder.Entity<ProductAward>(entity =>
            {
                entity.HasKey(x => x.ID).IsClustered(false);
                entity.Property(x => x.SequenceNo).UseIdentityColumn().ValueGeneratedOnAdd();
                entity.Property(x => x.CreateTime).HasColumnType("datetime2(7)");
                entity.Property(x => x.UpdateTime).HasColumnType("datetime2(7)");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
