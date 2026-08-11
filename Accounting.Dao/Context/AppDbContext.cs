using Microsoft.EntityFrameworkCore;

namespace Accounting.Dao.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Account> Accounts { get; set; }
        public DbSet<Entities.JournalEntry> JournalEntries { get; set; }
        public DbSet<Entities.JournalEntryLine> JournalEntryLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
