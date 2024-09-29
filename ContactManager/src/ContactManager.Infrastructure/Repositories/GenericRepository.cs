using Ardalis.GuardClauses;
using ContactManager.Domain.Common;
using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactManager.Infrastructure.Repositories;

public class GenericRepository<TEntity>
    : IRepository<TEntity> where TEntity : BaseAuditableEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        var existingEntity = GetById(entity.Id);

        Guard.Against.NotFound(entity.Id, existingEntity);
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var existingEntity = GetById(entity.Id);

        Guard.Against.NotFound(entity.Id, existingEntity);
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public void DeleteById(Guid id)
    {
        var entity = GetById(id);

        Guard.Against.NotFound(id, entity);
        entity.IsActive = false;
        Update(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = GetById(id);

        Guard.Against.NotFound(id, entity);
        entity.IsActive = false;
        await UpdateAsync(entity);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().Where(x => x.IsActive);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.Run(() => _context.Set<TEntity>().Where(x => x.IsActive));
    }

    public TEntity? GetById(Guid id)
    {
        return _context.Set<TEntity>().FirstOrDefault(x => x.IsActive && x.Id == id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.IsActive && x.Id == id);
    }

    public TEntity? Select(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().FirstOrDefault(predicate);
    }

    public Task<TEntity?> SelectAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }
}
