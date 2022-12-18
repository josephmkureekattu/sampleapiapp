using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sampleapp.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RunLog>().Property(e => e.Id)
                .UseIdentityColumn(seed: 1, increment: 1);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RunLog> Users { get; set; }
    }
}

public class RunLog
{
    public int Id { get; set; }
    public DateTime EntryDate { get; set; } = System.DateTime.Now;
}
