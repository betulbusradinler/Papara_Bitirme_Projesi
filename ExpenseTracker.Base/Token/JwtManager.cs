using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Base;

public static class JwtManager
{
    public static AppSession GetSession(HttpContext context)
    {
        AppSession session = new AppSession();
        var identity = context?.User?.Identity as ClaimsIdentity;
        if (identity == null)
            return session;

        var claims = identity.Claims;
        session.UserName = GetClaimValue(claims, "UserName");
        session.PersonnelId = GetClaimValue(claims, "PersonnelId");
        session.Role = GetClaimValue(claims, "Role");
        session.FirstName = GetClaimValue(claims, "FirstName");
        session.LastName = GetClaimValue(claims, "LastName");
        session.Token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        session.HttpContext = context;
        return session;
    }
    
    private static string GetClaimValue(IEnumerable<Claim> claims, string name)
    {
        var claim = claims.FirstOrDefault(c => c.Type == name);
        return claim?.Value;
    }
}