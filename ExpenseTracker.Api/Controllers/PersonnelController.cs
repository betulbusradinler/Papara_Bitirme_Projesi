using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Cqrs;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonnelController : ControllerBase
{
    private readonly IMediator _mediator;
    public PersonnelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("test");
    }   
    
    [HttpGet("{id}")]
    public IActionResult GetPersonnelById(int Id)
    {
        return Ok("test");
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PersonnelRequest personnelRequest)
    {
        var operation = new CreatePersonnelCommand(personnelRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PersonnelRequest personnelRequest)
    {
        // var operation = new UpdatePersonnelCommand(id,personnelRequest);
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
