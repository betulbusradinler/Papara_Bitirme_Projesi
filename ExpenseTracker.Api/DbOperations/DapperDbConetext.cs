
using System.Data;
using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Api.DbOperations;
public class DapperContext
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = configuration.GetConnectionString("MsSqlConnection");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(connectionString);
}
