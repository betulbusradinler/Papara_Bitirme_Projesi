using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Service;

namespace ExpenseTracker.Api.Impl.Command;

public class AuthCommandHandler :
IRequestHandler<CreateAuthTokenCommand, ApiResponse<AuthResponse>>
{
    private readonly ExpenseTrackDbContext dbContext;
    private readonly ITokenService tokenService;
    private readonly JwtConfig jwtConfig;

    public AuthCommandHandler(ExpenseTrackDbContext dbContext, ITokenService tokenService, JwtConfig jwtConfig)
    {
        this.dbContext = dbContext;
        this.tokenService = tokenService;
        this.jwtConfig = jwtConfig;
    }
    
    public async Task<ApiResponse<AuthResponse>> Handle(CreateAuthTokenCommand request, CancellationToken cancellationToken)
    {
        var personnel = await dbContext.Set<Personnel>().Include(p=>p.PersonnelPassword)
                    .FirstOrDefaultAsync(x => x.UserName == request.Auth.UserName, cancellationToken);

        if (personnel == null)
            return new ApiResponse<AuthResponse>("Personnel username or password is incorrect");

        // Verify Password
        bool isVerifyPassword = HashHelper.VerifyPasswordHash(request.Auth.Password, 
                                personnel.PersonnelPassword.Password, personnel.PersonnelPassword.Secret);
        if (!isVerifyPassword)
            return new ApiResponse<AuthResponse>("User name or password is incorrect");

        // Create Token
        var token = tokenService.GenerateToken(personnel);
        var entity = new AuthResponse
        {
            UserName = personnel.UserName,
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(jwtConfig.AccessTokenExpiration)
        };
        return new ApiResponse<AuthResponse>(entity);
    }   
}