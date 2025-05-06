using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.DbOperations;
public static class DbInitializer
{
    public static async Task InitializeAsync(ExpenseTrackDbContext context)
    {
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
                    SELECT 
                        p.FirstName,
                        p.LastName,
                        e.Demand,
                        ed.Amount,
                        e.RejectDescription,
                        ed.PaymentPoint,
                        ed.PaymentInstrument,
                        ed.Receipt,
                        pc.Name AS CategoryName,
                        CAST(e.UpdatedDate AS DATE) AS ReportDate,
                        SUM(ed.Amount) AS TotalAmount,
                        COUNT(*) AS ExpenseCount
                    FROM Expenses e 
                    JOIN ExpenseDetails ed ON ed.ExpenseId = e.Id
                    JOIN PaymentCategories pc ON pc.Id = e.PaymentCategoryId
                    JOIN Personnels p ON p.Id = e.StaffId
                    WHERE e.UpdatedDate BETWEEN @StartDate AND @EndDate
                    AND e.Demand IN (1, 2)
                    AND e.IsActive = 1
                    GROUP BY 
                        p.FirstName,
                        p.LastName,
                        e.Demand,
                        ed.Amount,
                        e.RejectDescription,
                        ed.PaymentPoint,
                        ed.PaymentInstrument,
                        ed.Receipt,
                        pc.Name,
                        CAST(e.UpdatedDate AS DATE)
                    END
                ')
            END
        ";
        var weeklyExpenseReportSpSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetMonthlyExpenseSummary')
            BEGIN
                EXEC('
                    CREATE PROCEDURE sp_GetMonthlyExpenseSummary
                        @StartDate DATE,
                        @EndDate DATE
                    AS
                    BEGIN
                        SELECT 
                            p.FirstName,
                            p.LastName,
                            e.Demand,
                            ed.Amount,
                            e.RejectDescription,
                            ed.PaymentPoint,
                            ed.PaymentInstrument,
                            ed.Receipt,
                            pc.Name AS CategoryName,
                            DATEPART(YEAR, e.UpdatedDate) AS ReportYear,
                            DATEPART(WEEK, e.UpdatedDate) AS ReportWeek,
                            SUM(ed.Amount) AS TotalAmount,
                            COUNT(*) AS ExpenseCount
                        FROM Expenses e 
                        JOIN ExpenseDetails ed ON ed.ExpenseId = e.Id
                        JOIN PaymentCategories pc ON pc.Id = e.PaymentCategoryId
                        JOIN Personnels p ON p.Id = e.StaffId
                        WHERE e.UpdatedDate BETWEEN @StartDate AND @EndDate
                        AND e.Demand IN (1, 2)
                        AND e.IsActive = 1
                        GROUP BY 
                            p.FirstName,
                            p.LastName,
                            e.Demand,
                            ed.Amount,
                            e.RejectDescription,
                            ed.PaymentPoint,
                            ed.PaymentInstrument,
                            ed.Receipt,
                            pc.Name,
                            DATEPART(YEAR, e.UpdatedDate),
                            DATEPART(WEEK, e.UpdatedDate)
                        END
                ')
            END
        ";

        var monthlyExpenseReportSpSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetMonthlyExpenseSummary')
            BEGIN
                EXEC('
                    CREATE PROCEDURE sp_GetMonthlyExpenseSummary
                        @StartDate DATE,
                        @EndDate DATE
                    AS
                    BEGIN
                    SELECT 
                        p.FirstName,
                        p.LastName,
                        e.Demand,
                        ed.Amount,
                        e.RejectDescription,
                        ed.PaymentPoint,
                        ed.PaymentInstrument,
                        ed.Receipt,
                        pc.Name AS CategoryName,
                        MONTH(e.UpdatedDate) AS ReportMonth,
                        YEAR(e.UpdatedDate) AS ReportYear,
                        SUM(ed.Amount) AS TotalAmount,
                        COUNT(*) AS ExpenseCount
                    FROM Expenses e 
                    JOIN ExpenseDetails ed ON ed.ExpenseId = e.Id
                    JOIN PaymentCategories pc ON pc.Id = e.PaymentCategoryId
                    JOIN Personnels p ON p.Id = e.StaffId
                    WHERE e.UpdatedDate BETWEEN @StartDate AND @EndDate
                    AND e.Demand IN (1, 2)
                    AND e.IsActive = 1
                    GROUP BY 
                        p.FirstName,
                        p.LastName,
                        e.Demand,
                        ed.Amount,
                        e.RejectDescription,
                        ed.PaymentPoint,
                        ed.PaymentInstrument,
                        ed.Receipt,
                        pc.Name,
                        MONTH(e.UpdatedDate),
                        YEAR(e.UpdatedDate)
                    END
                ')
            END
        ";
        await context.Database.ExecuteSqlRawAsync(personnelExpenseView);
        await context.Database.ExecuteSqlRawAsync(paymentReportSpSql);
        await context.Database.ExecuteSqlRawAsync(dailyExpenseReportSpSql);
        await context.Database.ExecuteSqlRawAsync(weeklyExpenseReportSpSql);
        await context.Database.ExecuteSqlRawAsync(monthlyExpenseReportSpSql);
    }
}
