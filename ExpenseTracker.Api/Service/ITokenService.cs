using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api.Service;

public  interface ITokenService
{
    public string GenerateToken(Personnel personnel);
}