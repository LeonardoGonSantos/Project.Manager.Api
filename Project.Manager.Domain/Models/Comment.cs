using System.Diagnostics.CodeAnalysis;

namespace Project.Manager.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}