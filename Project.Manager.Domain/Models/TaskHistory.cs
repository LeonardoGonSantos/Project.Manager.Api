namespace Project.Manager.Api.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public string Change { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }
    }
}