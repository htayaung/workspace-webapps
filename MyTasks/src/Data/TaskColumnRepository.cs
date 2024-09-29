using MyTasks.Models;

namespace MyTasks.Data
{
    public class TaskColumnRepository : GenericRepository<TaskColumn>, ITaskColumnRepository
    {
        public TaskColumnRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
