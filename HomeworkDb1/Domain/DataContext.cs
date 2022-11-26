using HomeworkDb1.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HomeworkDb1.Domain;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options ) : base(options)
    {
    }

    //public DbSet<User> Users { get; set; }
    public DbSet<Lector> Lectors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
}