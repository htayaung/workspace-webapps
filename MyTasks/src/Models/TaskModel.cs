namespace MyTasks.Models
{
    public class TaskModel : BaseModel<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsArchived { get; set; }

        public Guid TaskColumnId { get; set; }

        public Guid? TaskTagId { get; set; }
    }
}
