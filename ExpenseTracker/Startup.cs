using Microsoft.EntityFrameworkCore;
using ExpenseTracker.DbOperations;
using ExpenseTracker.Mapper;
using ExpenseTracker.Impl.Cqrs;

namespace ExpenseTracker;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;    
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // DB DI
        services.AddDbContext<ExpenseTrackDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection"));
        });

        // MediatR
    
         services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreatePaymentCategoryCommand).Assembly));

        // AutoMapper
        services.AddAutoMapper(typeof(MapperConfiguration));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        //app.Run();

    }
}


