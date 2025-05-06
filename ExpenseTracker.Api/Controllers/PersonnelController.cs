using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Cqrs;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace ExpenseTracker.Api;

//[Authorize]
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
    public async Task<IActionResult> Get()
    {
        var operation = new GetAllPersonnelQuery();
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var operation = new GetPersonnelByIdQuery(id);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PersonnelRequest personnelRequest)
    {
        var operation = new CreatePersonnelCommand(personnelRequest);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PersonnelRequest personnelRequest)
    {
        var operation = new UpdatePersonnelCommand(id, personnelRequest);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var operation = new DeletePersonnelCommand(id);
        var result = await _mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
}
