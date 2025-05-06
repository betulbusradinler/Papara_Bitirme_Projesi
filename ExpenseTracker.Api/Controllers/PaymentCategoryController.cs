using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api;

// [Authorize(Roles = "admin")]
[ApiController]
[Route("api/[controller]")]
public class PaymentCategoryController : ControllerBase
{
    // BURADA YÖNETİCİ İZNİ OLCAK. USER ROLE ADMINSE BU İŞLEMLERİ YAPABİLECEK
    
    private readonly IMediator _mediator;
    public PaymentCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetPaymentCategory")]
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllPaymentCategoryQuery();
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }   
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentCategory([FromRoute] int id)
    {
        var operation = new GetPaymentCategoryByIdQuery(id);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentCategoryRequest PaymentCategoryRequest)
    {
        var operation = new CreatePaymentCategoryCommand(PaymentCategoryRequest);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PaymentCategoryRequest paymentCategoryRequest)
    {
        var operation = new UpdatePaymentCategoryCommand(id,paymentCategoryRequest);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var operation = new DeletePaymentCategoryCommand(id);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
}
