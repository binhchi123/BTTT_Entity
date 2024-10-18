using e2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace e2.Data
{
    public class CookDbContext : DbContext
    {
        public CookDbContext(DbContextOptions<CookDbContext> options) : base(options)
        {
        }

        public DbSet<CongThuc> CongThucs { get; set; }
        public DbSet<LoaiMonAn> LoaiMonAns { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<NguyenLieu> NguyenLieus { get; set; }
    }
}
