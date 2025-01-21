using System.Diagnostics.CodeAnalysis;

namespace Project.Manager.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<TaskHistory> History { get; set; } = new List<TaskHistory>();
    }
}