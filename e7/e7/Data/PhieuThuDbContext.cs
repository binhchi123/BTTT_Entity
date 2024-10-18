using e7.Models;
using Microsoft.EntityFrameworkCore;

namespace e7.Data
{
    public class PhieuThuDbContext : DbContext
    {
        public PhieuThuDbContext(DbContextOptions<PhieuThuDbContext> options) : base(options) { }
        public DbSet<LoaiNguyenLieu> LoaiNguyenLieus { get; set; }
        public DbSet<NguyenLieu> NguyenLieus { get; set; }
        public DbSet<PhieuThu> PhieuThus { get; set; }
        public DbSet<ChiTietPhieuThu> ChiTietPhieuThus { get; set; }
    }
}
