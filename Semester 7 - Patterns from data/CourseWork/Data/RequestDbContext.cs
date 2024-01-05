using Microsoft.EntityFrameworkCore;

namespace CourseWork.Data;

public class RequestDbContext:DbContext
{
    public DbSet<Request> Requests { get; set; }

    public RequestDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Request>().ToTable("Requests");

        modelBuilder.Entity<Request>().HasKey(x => x.RequestId);
    }
}