using ExpenseTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.DbOperations;
public static class DbInitializer
{
    public static async Task InitializeAsync(ExpenseTrackDbContext context)
    {
        if (context.Personnels.Any())
        {
            return;
        }

        await context.Personnels.AddRangeAsync(
            new Personnel
            {
                Role = "Admin",
                UserName = "admin",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@admin.com",
                Iban = "TR1234567890",
                OpenDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedUser = "System",
                IsActive = true
            },
            new Personnel
            {
                Role = "Personnel",
                UserName = "personel",
                FirstName = "John",
                LastName = "Doe",
                Email = "personel@personel.com",
                Iban = "TR0987654321",
                OpenDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedUser = "System",
                IsActive = true
            }
        );

        await context.SaveChangesAsync();

        var personnelExpenseView = @"
            IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_PersonnelExpenses')
                BEGIN
                    EXEC('
                    CREATE VIEW vw_PersonnelExpenses AS
                    SELECT 
                        e.Id AS ExpenseId,
                        e.StaffId,
                        ed.Description,
                        ed.PaymentPoint,
                        ed.PaymentInstrument,
                        ed.Receipt,
                        ed.Amount,
                        e.RejectDescription,
                        e.Demand,
                        py.PaymentDate,
                        pc.Name AS CategoryName
                    FROM 
                        Expenses e
                    JOIN 
                        Personnels p ON p.Id = e.StaffId
                    JOIN 
                        PaymentCategories pc ON pc.Id = e.PaymentCategoryId
                    LEFT JOIN 
                        Payments py ON py.ExpenseId = e.Id
                    LEFT JOIN 
                        ExpenseDetails ed ON ed.ExpenseId = e.Id
                    WHERE 
                        e.IsActive = 1
                    ')
                END
        ";

        var paymentReportSpSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPaymentSummaryDetails')
            BEGIN
                EXEC('
                    CREATE PROCEDURE sp_GetPaymentSummaryDetails
                        @StartDate DATE,
                        @EndDate DATE
                    AS
                    BEGIN
                        SELECT 
                            p.FirstName,
                            p.LastName,
                            py.PaymentDate,
                            ed.PaymentPoint,
                            ed.PaymentInstrument,
                            ed.Receipt,
                            ed.Amount,
                            pc.Name AS CategoryName
                        FROM Payments py
                        JOIN Expenses e ON e.Id = py.ExpenseId
                        JOIN ExpenseDetails ed ON ed.ExpenseId = e.Id
                        JOIN PaymentCategories pc ON pc.Id = e.PaymentCategoryId
                        JOIN Personnels p ON p.Id = e.StaffId
                        WHERE CAST(py.PaymentDate AS DATE) BETWEEN  @StartDate AND @EndDate
                        AND e.IsActive = 1
                    END
                ')
            END
        ";

        var dailyExpenseReportSpSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetDailyExpenseSummary')
            BEGIN
                EXEC('
                CREATE PROCEDURE sp_GetDailyExpenseSummary
                    @StartDate DATE,
                    @EndDate DATE
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT 
                        e.Demand AS DemandState,
                        py.Name AS CategoryName,
                        e.UpdatedDate AS Daily,
                        SUM(ed.Amount) AS SumExpense
                    FROM 
                        Expenses e
                    JOIN 
                        ExpenseDetails ed ON e.Id = ed.ExpenseId
                    JOIN 
                        PaymentCategories py ON py.Id = e.PaymentCategoryId
                    WHERE
                        e.Demand BETWEEN 1 AND 2
                        AND CAST(e.UpdatedDate AS DATE) BETWEEN @StartDate AND @EndDate
                    GROUP BY 
                        e.UpdatedDate,
                        e.Demand,
                        py.Name
                    ORDER BY 
                        Daily DESC;
                END
                ')
            END
        ";

        var weeklyAndMonthlyExpenseReportSpSql = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWeeklyAndMonthlyExpenseReport')
            BEGIN
                EXEC('
                    CREATE PROCEDURE sp_GetWeeklyAndMonthlyExpenseReport
                        @StartDate DATE,
                        @EndDate DATE
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        SELECT 
                            e.Demand AS DemandState,
                            py.Name AS CategoryName,
                            DATEPART(YEAR, e.UpdatedDate) AS Year,
                            DATEPART(WEEK, e.UpdatedDate) AS Weekly,
                            DATEPART(MONTH, e.UpdatedDate) AS Monthly,
                            SUM(ed.Amount) AS SumExpense
                        FROM 
                            Expenses e
                        JOIN 
                            ExpenseDetails ed ON e.Id = ed.ExpenseId
                        JOIN 
                            PaymentCategories py ON py.Id = e.PaymentCategoryId
                        WHERE
                            e.Demand BETWEEN 1 AND 2
                            AND CAST(e.UpdatedDate AS DATE) BETWEEN @StartDate AND @EndDate
                        GROUP BY 
                            DATEPART(YEAR, e.UpdatedDate), 
                            DATEPART(MONTH, e.UpdatedDate),
                            DATEPART(WEEK, e.UpdatedDate),
                            e.Demand,
                            py.Name
                        ORDER BY 
                            Year DESC, Monthly DESC, Weekly DESC;
                    END
                ')
            END
            ";

        await context.Database.ExecuteSqlRawAsync(personnelExpenseView);
        await context.Database.ExecuteSqlRawAsync(paymentReportSpSql);
        await context.Database.ExecuteSqlRawAsync(dailyExpenseReportSpSql);
        await context.Database.ExecuteSqlRawAsync(weeklyAndMonthlyExpenseReportSpSql);
    }

}