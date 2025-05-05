using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using MediatR;

namespace ExpenseTracker.Api.Impl.Cqrs;

public class Report
{
    public record GetAllReportQuery : IRequest<ApiResponse<List<ReportResponse>>>;
    public record GetAllReportByIdQuery(int Id) : IRequest<ApiResponse<ReportResponse>>;
    public record CreateReportCommand(ReportResponse Report) : IRequest<ReportResponse>;
    public record UpdateReportCommand(int Id, ReportResponse Report) : IRequest<ApiResponse>;
    public record DeleteReportCommand(int Id) : IRequest<ApiResponse>;

}