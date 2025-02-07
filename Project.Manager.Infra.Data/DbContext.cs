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
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TaskItem> TaskItems { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<TaskHistory> TaskHistories { get; set; }
    }
}