using System.Linq.Expressions;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task SaveChangesAsync();
    Task<TEntity> GetByIdAsync(int id, params string[] includes);
    Task<List<TEntity>> GetAllAsync(params string[] includes);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    Task<TEntity> AddAsync(TEntity entity);
    Task<List<TEntity>> AddRangeAsync(List<TEntity> entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteByIdAsync(int id);
}
