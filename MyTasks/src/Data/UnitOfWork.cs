namespace MyTasks.Data;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;

        TaskTag = new TaskTagRepository(context);
        TaskColumn = new TaskColumnRepository(context);
        Task = new TaskRepository(context);
    }

    public ITaskTagRepository TaskTag { get; private set; }

    public ITaskColumnRepository TaskColumn { get; private set; }

    public ITaskRepository Task { get; private set; }

    public void Dispose()
    {
        Dispose(disposing: true);
    }

    public int Save()
    {
        return context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }
}
