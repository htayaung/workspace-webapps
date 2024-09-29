namespace MyTasks.Models
{
    public class TaskTag : BaseModel<Guid>
    {
        public string Name { get; set; }

        public string? HexCode { get; set; }
    }
}
