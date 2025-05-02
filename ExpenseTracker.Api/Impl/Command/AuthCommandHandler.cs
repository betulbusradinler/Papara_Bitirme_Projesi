using MediatR;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Service;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Command;

public class AuthCommandHandler :
IRequestHandler<CreateAuthTokenCommand, ApiResponse<AuthResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ITokenService tokenService;
    private readonly JwtConfig jwtConfig;

    public AuthCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, JwtConfig jwtConfig)
    {
        this.unitOfWork = unitOfWork;
        this.tokenService = tokenService;
        this.jwtConfig = jwtConfig;
    }
    
    public async Task<ApiResponse<AuthResponse>> Handle(CreateAuthTokenCommand request, CancellationToken cancellationToken)
    {
        var personnel = await unitOfWork.PersonnelRepository.GetByUserNameAsync(request.Auth.UserName);
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