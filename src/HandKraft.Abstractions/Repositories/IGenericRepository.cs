using System.Linq.Expressions;
using HandKraft.Abstractions.Common;
using HandKraft.Abstractions.Entities;

namespace HandKraft.Abstractions.Repositories;

public interface IGenericRepository<TEntity> : IGenericRepository<TEntity, Guid> 
    where TEntity : Entity<Guid>
{
}

public interface IGenericRepository<TEntity, in TKey> where TEntity : Entity<TKey>
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<PagedData<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        int page = 1,
        int pageSize = 10);
    IQueryable<TEntity> AsQueryable();
    
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    
    void Delete(TEntity entity);
    Task DeleteByIdAsync(TKey id);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    void DeleteRange(IEnumerable<TEntity> entities);
    
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> GetTotalCountAsync(
        Expression<Func<TEntity, bool>>? predicate = null);
}
