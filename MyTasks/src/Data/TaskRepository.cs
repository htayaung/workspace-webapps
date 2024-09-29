using MyTasks.Models;

namespace MyTasks.Data
{
    public class TaskRepository : GenericRepository<MyTasks.Models.TaskModel>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
