using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Cqrs;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpenseController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet(Name = "GetExpenseDemands")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllExpenseQuery();
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Personnel")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ExpenseRequest ExpenseRequest)
    {
        var operation = new CreateExpenseCommand(ExpenseRequest);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [NonAction]
    [Authorize(Roles = "Personnel")]
    [HttpPost("AddExpenseList")]
    public async Task<IActionResult> ExpenseListPost([FromBody] List<ExpenseRequest> ExpenseRequests)
    {
        var operation = new CreateExpenseListCommand(ExpenseRequests);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Personnel")]
    [HttpPost("filter")]
    public async Task<IActionResult> GetFilteredExpenses([FromBody] ExpenseFilterRequest filter)
    {
        var operation = new GetFilteredExpensesQuery(filter);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Personnel")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ExpenseRequest ExpenseRequest)
    {
        var operation = new UpdateExpenseCommand(id, ExpenseRequest);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("ApproveOrReject/{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ApproveOrRejectExpenseRequest approveOrRejectExpense)
    {
        var operation = new ApproveOrRejectExpenseCommand(id, approveOrRejectExpense);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var operation = new DeleteExpenseCommand(id);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
}
