using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository
{
    public class PersonnelRepository : GenericRepository<Personnel>, IPersonnelRepository
    {
        private readonly ExpenseTrackDbContext dbContext;

        public PersonnelRepository(ExpenseTrackDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Personnel> GetByUserNameAsync(string UserName, params string[] includes)
        {
            var query = dbContext.Set<Personnel>()
                .Include(p=>p.PersonnelPassword)
                .AsQueryable();

            query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.UserName == UserName);
        }
        public async Task<Personnel> GetPersonnelDetailById(int Id, params string[] includes)
        {
            var query = dbContext.Set<Personnel>()
                .Include(p => p.PersonnelAddresses)
                .Include(p => p.PersonnelPhones)
                .AsQueryable();

            query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.Id == Id);
        }

    }
}