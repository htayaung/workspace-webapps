namespace MyTasks.Models
{
    public class TaskColumn : BaseModel<Guid>
    {
        public string Name { get; set; }

        public string? Color { get; set; }

        public int Position { get; set; }
    }
}
