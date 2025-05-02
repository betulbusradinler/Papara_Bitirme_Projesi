using System.Linq.Expressions;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly ExpenseTrackDbContext dbContext;
        public PersonnelRepository(ExpenseTrackDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Personnel> AddAsync(Personnel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Personnel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Personnel>> GetAllAsync(params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<Personnel>> GetAllAsync(Expression<Func<Personnel, bool>> predicate, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Personnel> GetByIdAsync(int id, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<Personnel> GetByUserNameAsync(string UserName, params string[] includes)
        {
            var query = dbContext.Set<Personnel>().Include(p=>p.PersonnelPassword).AsQueryable();
            query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.UserName == UserName);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Personnel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Personnel>> Where(Expression<Func<Personnel, bool>> predicate, params string[] includes)
        {
            throw new NotImplementedException();
        }
    }
}