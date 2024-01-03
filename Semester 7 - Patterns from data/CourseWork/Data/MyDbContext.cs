using Microsoft.EntityFrameworkCore;

namespace CourseWork.Data;

public class MyDbContext:DbContext
{
    public DbSet<Request> Requests { get; set; }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Request>().ToTable("Requests");

        modelBuilder.Entity<Request>().HasKey(x => x.RequestId);
    }
}