using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Cqrs;

namespace ExpenseTracker.Api;

[NonController]
// [Authorize(Roles = "admin")]
[ApiController]
[Route("api/[controller]")]
public class PersonnelAddressController : ControllerBase
{
    // BURADA YÖNETİCİ İZNİ OLCAK. USER ROLE ADMINSE BU İŞLEMLERİ YAPABİLECEK
    private readonly IMediator _mediator;
    public PersonnelAddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("test");
    }   
    
    [HttpGet("{id}")]
    public IActionResult GetPersonnelAddressById(int id)
    {
        return Ok("test");
    }


    [HttpPost]
    public async Task<IActionResult> Post(int Id, [FromBody] PersonnelAddressRequest personnelAddressRequest)
    {
        var operation = new CreatePersonnelAddressCommand(Id,personnelAddressRequest);
        var result = await _mediator.Send(operation);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PersonnelAddressRequest personnelAddressRequest)
    {
        // var operation = new UpdatePersonnelAddressCommand(id,personnelAddressRequest);
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
