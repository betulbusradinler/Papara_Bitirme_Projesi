using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository
{
    public interface IPersonnelRepository:IGenericRepository<Personnel>
    {
        Task<Personnel> GetByUserNameAsync(string UserName, params string[] includes);
    }
}