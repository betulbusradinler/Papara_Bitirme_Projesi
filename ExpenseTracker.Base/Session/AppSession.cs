using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Base;

public class AppSession: IAppSession
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string PersonnelId { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public HttpContext HttpContext { get; set; }
}
