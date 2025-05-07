IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE type = 'P' AND name = 'sp_GetPaymentSummaryDetails'
)
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
            WHERE CAST(py.PaymentDate AS DATE) BETWEEN @StartDate AND @EndDate
            AND e.IsActive = 1
        END
    ');
END;


IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetDailyExpenseSummary')
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
                END');
            END;
            
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
                ');
            END;