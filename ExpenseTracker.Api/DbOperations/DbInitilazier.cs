namespace ExpenseTracker.Api.DbOperations;
public static class DbInitializer
{
    public static async Task InitializeAsync(ExpenseTrackDbContext context)
    {
        var createViewSql = @"
            IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_ExpenseSummary')
            BEGIN
                EXEC('
                    CREATE VIEW vw_ExpenseSummary AS
                    SELECT StaffId, SUM(Amount) AS TotalAmount
                    FROM Expenses
                    GROUP BY StaffId
                ')
            END
        ";
        var personnelExpenseView = @"
            IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_PersonnelExpenses')
            BEGIN
                EXEC('
                   CREATE VIEW vw_PersonnelExpenses AS
                    SELECT 
                        e.Id AS ExpenseId,
                        e.StaffId,
                        p.FullName,
                        e.Amount,
                        e.Demand,
                        e.PaymentDate,
                        pc.Name AS CategoryName
                    FROM Expenses e
                    JOIN Personnel p ON e.StaffId = p.Id
                    JOIN PaymentCategories pc ON e.PaymentCategoryId = pc.Id
                    JOIN ExpenseDetails ed ON e.ExpenseId
                    WHERE e.IsActive = 1
                ')
            END
        ";
        var createProcedureSql = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWeeklyExpenses')
            BEGIN
                EXEC('
                    CREATE PROCEDURE sp_GetWeeklyExpenses
                    AS
                    BEGIN
                        SELECT * FROM Expenses
                        WHERE CreatedDate >= DATEADD(DAY, -7, GETDATE())
                    END
                ')
            END
        ";

        // await context.Database.ExecuteSqlRawAsync(createViewSql);
        // await context.Database.ExecuteSqlRawAsync(createProcedureSql);
    }
}
