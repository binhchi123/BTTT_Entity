using e4.Models;
using Microsoft.EntityFrameworkCore;

namespace e4.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<Lop> Lops { get; set; }
    }
}
