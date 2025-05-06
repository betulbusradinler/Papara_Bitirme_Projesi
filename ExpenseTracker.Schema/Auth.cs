namespace ExpenseTracker.Schema;

public class AuthRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class AuthResponse 
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public DateTime Expiration { get; set; }
}