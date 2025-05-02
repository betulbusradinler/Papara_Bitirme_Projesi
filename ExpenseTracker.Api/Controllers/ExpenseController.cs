using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Domain;

namespace ExpenseTracker;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetExpenseDemands")]
    public IActionResult Get()
    {
        // Tüm kullanıcıların Harcamalarını getirir. 
        return Ok("test");
    }
    
    [HttpGet("{id}")]
    public IActionResult GetExpenseById(int Id)
    {
        // İstenen harcamayı detayı ile birlikte getirir.
        return Ok("test");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ExpenseRequest ExpenseRequest)
    {
        var operation = new CreateExpenseCommand(ExpenseRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
    
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PaymentCategoryRequest paymentCategoryRequest)
    // {
    //     var operation = new UpdatePaymentCategoryCommand(id,paymentCategoryRequest);
    //     var result = await _mediator.Send(operation);
    //     return Ok(result);
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete([FromRoute] int id)
    // {
    //     var operation = new DeletePaymentCategoryCommand(id);
    //     var result = await _mediator.Send(operation);
    //     return Ok(result);
    // }
}
