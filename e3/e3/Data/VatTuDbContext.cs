using e3.Models;
using Microsoft.EntityFrameworkCore;

namespace e3.Data
{
    public class VatTuDbContext : DbContext
    {
        public VatTuDbContext(DbContextOptions<VatTuDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<VatTu> VatTus { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<PhieuXuat> PhieuXuats { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}
