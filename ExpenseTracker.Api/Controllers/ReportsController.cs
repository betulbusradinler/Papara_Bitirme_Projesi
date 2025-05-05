using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    // private readonly IReportRepository _reportRepository;
    private readonly IAppSession _appSession;
    private readonly IMediator mediator;
    public ReportsController(IMediator mediator, IAppSession appSession)
    {
        this.mediator = mediator;
        _appSession = appSession;
    }

    [HttpGet("personnel")]
    public async Task<IActionResult> GetPersonnelReport()
    {
        var operation = new GetAllPersonnelExpenseQuery();
        var result = await mediator.Send(operation);
        return Ok(result);
    }

    [HttpGet("company")]
    public async Task<IActionResult> GetCompanyReport(DateTime start, DateTime end)
    {
        return Ok();
    }
}
