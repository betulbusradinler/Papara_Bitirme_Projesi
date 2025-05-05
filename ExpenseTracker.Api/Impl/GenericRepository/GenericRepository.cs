using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ExpenseTrackDbContext dbContext;

    public GenericRepository(ExpenseTrackDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
    {
        await dbContext.Set<TEntity>().AddRangeAsync(entity);
        return entity;
    }

    public async Task DeleteByIdAsync(long id)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAllAsync(params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().Where(predicate).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }

    public async Task<TEntity> GetByIdAsync(int id, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().Where(predicate).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
