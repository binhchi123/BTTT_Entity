using e5.Models;
using Microsoft.EntityFrameworkCore;

namespace e5.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<DuAn> DuAns { get; set; }
        public DbSet<PhanCong> PhanCongs { get; set; }
    }
}
