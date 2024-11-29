using _09_Database_Connections.Models;
using Microsoft.EntityFrameworkCore;

namespace _09_Database_Connections.Data
{
    public class DataContextEF : DbContext
    {
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true",
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