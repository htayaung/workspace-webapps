using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(5), MaxLength(3000)]
        public string Description { get; set; }

        public bool IsArchived { get; set; }

        public Guid TaskColumnId { get; set; }

        public Guid? TaskTagId { get; set; }

        public string? TagName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime LastUpdatedOn
        {
            get
            {
                return UpdatedOn ?? CreatedOn;
            }
        }

        public static TaskViewModel FromEntity(TaskModel entity)
        {
            return new TaskViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsArchived = entity.IsArchived,
                TaskColumnId = entity.TaskColumnId,
                TaskTagId = entity.TaskTagId,
                CreatedOn = entity.CreatedOn,
                UpdatedOn = entity.UpdatedOn
            };
        }

        public static IEnumerable<TaskViewModel> FromEntities(IEnumerable<TaskModel> entities)
        {
            return entities.Select(entity => FromEntity(entity));
        }

        public TaskModel ToEntity()
        {
            return new TaskModel
            {
                Id = Id,
                Name = Name,
                Description = Description,
                IsArchived = IsArchived,
                TaskColumnId = TaskColumnId,
                TaskTagId = TaskTagId
            };
        }

        public void CopyTo(TaskViewModel target)
        {
            target.Id = Id;
            target.Name = Name;
            target.Description = Description;
            target.IsArchived = IsArchived;
            target.TaskColumnId = TaskColumnId;
            target.TaskTagId = TaskTagId;
            target.CreatedOn = CreatedOn;
            target.UpdatedOn = UpdatedOn;
        }
    }
}
