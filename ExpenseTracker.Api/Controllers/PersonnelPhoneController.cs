using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api;

[NonController]
// [Authorize(Roles = "admin")]
[ApiController]
[Route("api/[controller]")]
public class PersonnelPhoneController : ControllerBase
{
    private readonly IMediator _mediator;
    public PersonnelPhoneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("test");
    }   
    
    [HttpGet("{id}")]
    public IActionResult GetPersonnelPhoneById(int Id)
    {
        return Ok("test");
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PersonnelPhoneRequest personnelPhoneRequest)
    {
        var operation = new CreatePersonnelPhoneCommand(personnelPhoneRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PersonnelPhoneRequest personnelPhoneRequest)
    {
        // var operation = new UpdatePersonnelPhoneCommand(id,personnelPhoneRequest);
        // var result = await _mediator.Send(operation);
        return Ok("test");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        // var operation = new DeletePaymentCategoryCommand(id);
        // var result = await _mediator.Send(operation);
        return Ok("result");
    }
}
