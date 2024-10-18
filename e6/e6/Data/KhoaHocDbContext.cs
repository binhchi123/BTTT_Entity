using e6.Models;
using Microsoft.EntityFrameworkCore;

namespace e6.Data
{
    public class KhoaHocDbContext : DbContext
    {
        public KhoaHocDbContext(DbContextOptions<KhoaHocDbContext> options) : base(options) { }

        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<NgayHoc> NgayHocs { get; set; }
    }
}
