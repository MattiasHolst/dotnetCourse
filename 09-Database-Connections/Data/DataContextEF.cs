using _09_Database_Connections.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _09_Database_Connections.Data
{
    public class DataContextEF(IConfiguration config) : DbContext
    {
        private IConfiguration _config = config;

        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
            //.HasNoKey();
            // .ToTable("Computer", "TutorialAppSchema");
            // .ToTable("TableName", "SchemaName");
        }
    }
}