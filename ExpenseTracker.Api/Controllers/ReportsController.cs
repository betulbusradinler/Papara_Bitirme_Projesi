using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Schema;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IMediator mediator;
    public ReportsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "Personnel")]
    [HttpGet("me")]
    public async Task<IActionResult> GetPersonnelReport()
    {
        var operation = new GetPersonnelExpenseReportQuery();
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("CompanyPayment")]
    public async Task<IActionResult> GetPaymentSummary([FromQuery] PaymentSummaryRequest request)
    {
        var operation = new GetCompanyPaymentReportQuery(request);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("ExpenseSummary")]
    public async Task<IActionResult> GetExpenseSummary([FromQuery] ExpenseSummaryReportRequest request)
    {
        var operation = new GetAllExpenseReportQuery(request);
        var result = await mediator.Send(operation);
        if (result.Success == false)
            return StatusCode(result.Status, result.Message);
        return Ok(result);
    }
}
