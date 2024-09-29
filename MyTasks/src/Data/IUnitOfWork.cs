namespace MyTasks.Data;

public interface IUnitOfWork : IDisposable
{
    ITaskTagRepository TaskTag { get; }

    ITaskColumnRepository TaskColumn { get; }

    ITaskRepository Task { get; }

    int Save();

    Task<int> SaveAsync();
}
