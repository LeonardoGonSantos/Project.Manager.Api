using Microsoft.EntityFrameworkCore;

namespace Project.Manager.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class DbContext : DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
    }
}