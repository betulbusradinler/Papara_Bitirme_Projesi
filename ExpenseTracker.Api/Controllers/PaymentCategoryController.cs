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
    public IActionResult Get()
    {
        return Ok("test");
    }   
    
    [HttpGet("{id}")]
    public IActionResult GetPaymentCategory(int Id)
    {
        return Ok("test");
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentCategoryRequest PaymentCategoryRequest)
    {
        var operation = new CreatePaymentCategoryCommand(PaymentCategoryRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PaymentCategoryRequest paymentCategoryRequest)
    {
        var operation = new UpdatePaymentCategoryCommand(id,paymentCategoryRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var operation = new DeletePaymentCategoryCommand(id);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
}
