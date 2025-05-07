using ExpenseTracker.Api.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api;

public class Program
{
    public static void RunCustomSqlScripts(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ExpenseTrackDbContext>();
        var connection = context.Database.GetDbConnection();

        connection.Open();

        string[] scriptFiles = new[]
        {
            "DbOperations/DbScripts/CreateViews.sql",
            "DbOperations/DbScripts/CreateStoredProcedures.sql"
        };

        foreach (var scriptPath in scriptFiles)
        {
            if (File.Exists(scriptPath))
            {
                var script = File.ReadAllText(scriptPath);

                if (string.IsNullOrWhiteSpace(script))
                {
                    Console.WriteLine($"Uyarı: {scriptPath} dosyası boş.");
                    continue;
                }

                using var command = connection.CreateCommand();
                command.CommandText = script;

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"{scriptPath} başarıyla çalıştırıldı.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {scriptPath} dosyası çalıştırılamadı. Hata: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Hata: {scriptPath} dosyası bulunamadı.");
            }
        }

        connection.Close();
    }

    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ExpenseTrackDbContext>();
            db.Database.Migrate();
            RunCustomSqlScripts(scope.ServiceProvider);
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
