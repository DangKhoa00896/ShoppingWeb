using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Models;

namespace ShoppingWeb.Data
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options) { }

        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SanPham>()
                .HasOne(s => s.DanhMuc)
                .WithMany(d => d.SanPhams)
                .HasForeignKey(s => s.DanhMucMa);
        }
    }
}