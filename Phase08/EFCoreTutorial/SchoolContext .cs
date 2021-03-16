using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}