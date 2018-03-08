using Geonorge.Endringslogg.Models;
using Microsoft.EntityFrameworkCore;

namespace Geonorge.Endringslogg.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Application> Applications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .HasIndex(a => a.ApiKey)
                .IsUnique();

            modelBuilder.Entity<LogEntry>().HasIndex(l => l.DateTime);
            modelBuilder.Entity<LogEntry>().HasIndex(l => l.Application);
            modelBuilder.Entity<LogEntry>().HasIndex(l => l.ElementId);
            modelBuilder.Entity<LogEntry>().HasIndex(l => l.User);
            modelBuilder.Entity<LogEntry>().HasIndex(l => l.Operation);
        }
    }
}