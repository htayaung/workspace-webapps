using MyTasks.Models;

namespace MyTasks.Data
{
    public class TaskTagRepository : GenericRepository<TaskTag>, ITaskTagRepository
    {
        public TaskTagRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
