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
    ');
END;
