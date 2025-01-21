using System.Diagnostics.CodeAnalysis;

namespace Project.Manager.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}