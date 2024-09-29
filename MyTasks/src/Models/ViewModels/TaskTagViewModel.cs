using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models.ViewModels
{
    public class TaskTagViewModel
    {
        public Guid Id { get; set; }

        public int SerialNo { get; set; }

        [Required]
        [MinLength(5), MaxLength(100)]
        public string Name { get; set; }

        public string? HexCode { get; set; }

        public static TaskTagViewModel FromEntity(TaskTag entity)
        {
            return new TaskTagViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                HexCode = entity.HexCode
            };
        }

        public static IEnumerable<TaskTagViewModel> FromEntities(IEnumerable<TaskTag> entities)
        {
            return entities.Select(entity => FromEntity(entity));
        }

        public TaskTag ToEntity()
        {
            return new TaskTag
            {
                Id = Id,
                Name = Name,
                HexCode = HexCode
            };
        }

        public void CopyTo(TaskTagViewModel target)
        {
            target.Id = Id;
            target.Name = Name;
            target.HexCode = HexCode;
        }
    }
}
