using Microsoft.EntityFrameworkCore;

namespace CourseWork.Data;

public class MyDbContext:DbContext
{
    public DbSet<DataEntry> DataSet { get; set; }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DataEntry>().ToTable("DataSet");

        modelBuilder.Entity<DataEntry>().HasKey(x => x.RequestId);
    }
}