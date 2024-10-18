using e1.Models;
using Microsoft.EntityFrameworkCore;

namespace e1.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
