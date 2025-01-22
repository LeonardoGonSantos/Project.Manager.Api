using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Project.Manager.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class DataDbContext : DbContext
    {
        public DataDbContext() { }


        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                @"Data Source=ProjectManager");
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
    }
}