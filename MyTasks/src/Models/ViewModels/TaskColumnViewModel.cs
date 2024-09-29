using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models.ViewModels
{
    public class TaskColumnViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        public string? Color { get; set; }

        [Range(1, 10)]
        public int Position { get; set; }

        public static TaskColumnViewModel FromEntity(TaskColumn entity)
        {
            return new TaskColumnViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                Position = entity.Position
            };
        }

        public static IEnumerable<TaskColumnViewModel> FromEntities(IEnumerable<TaskColumn> entities)
        {
            return entities.Select(entity => FromEntity(entity));
        }

        public TaskColumn ToEntity()
        {
            return new TaskColumn
            {
                Id = Id,
                Name = Name,
                Color = Color,
                Position = Position
            };
        }

        public void CopyTo(TaskColumnViewModel target)
        {
            target.Id = Id;
            target.Name = Name;
            target.Color = Color;
            target.Position = Position;
        }
    }
}
