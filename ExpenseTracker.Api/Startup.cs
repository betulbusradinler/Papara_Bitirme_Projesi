using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Mapper;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Service;
using ExpenseTracker.Api.Impl.UnitOfWork;
using Microsoft.OpenApi.Models;
using ExpenseTracker.Api.Validation;
using MediatR;
using FluentValidation.AspNetCore;
using ExpenseTracker.Api.Impl.Middleware;

namespace ExpenseTracker.Api;
public class Startup
{
    public IConfiguration Configuration { get; }
    public static JwtConfig JwtConfig { get; private set; }
    public Startup(IConfiguration configuration) => Configuration = configuration;    
    public void ConfigureServices(IServiceCollection services)
    {
        JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.AddSingleton<JwtConfig>(JwtConfig);

        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<AuthValidator>();
        });

        services.AddHttpContextAccessor();
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

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPaymentService, PaymentService>();

        services.AddScoped<DapperContext>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                ValidAudience = JwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpenseTracker API", Version = "v1.0" });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "ExpenseTracker Management",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    { securityScheme, new string[] { } }
            });

        });

        services.AddScoped<IAppSession>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
            AppSession appSession = JwtManager.GetSession(httpContextAccessor.HttpContext);
            return appSession;
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseTracker API V1");
            c.RoutePrefix = string.Empty; // Swagger'ı root URL'e yönlendir
        });
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


    }
}