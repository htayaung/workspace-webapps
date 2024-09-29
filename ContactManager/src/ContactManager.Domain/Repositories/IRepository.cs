using ContactManager.Domain.Common;
using System.Linq.Expressions;

namespace ContactManager.Domain.Repositories;

public interface IRepository<TEntity> : IDisposable
    where TEntity : BaseAuditableEntity
{
    TEntity? GetById(Guid id);
    Task<TEntity?> GetByIdAsync(Guid id);
    IQueryable<TEntity> GetAll();
    Task<IQueryable<TEntity>> GetAllAsync();
    TEntity? Select(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SelectAsync(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity);
    void DeleteById(Guid id);
    Task DeleteByIdAsync(Guid id);
}
