using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Impl.Cqrs;
namespace ExpenseTracker.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Token")]
    public async Task<ApiResponse<AuthResponse>> Post([FromBody] AuthRequest request)
    {
        var operation = new CreateAuthTokenCommand(request);
        var result = await _mediator.Send(operation);
        return result;
    }
}