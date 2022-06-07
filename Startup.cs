using Microsoft.EntityFrameworkCore;
using ProjetoSupermercado.Application.Interfaces;
using ProjetoSupermercado.Application.Services;
using ProjetoSupermercado.Infrastructure;
using ProjetoSupermercado.Infrastructure.Data;
using ProjetoSupermercado.Models;
using System.Reflection;

namespace ProjetoSupermercado;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomDbContext(Configuration);
        services.AddControllers();
        services.AddAuthentication("CookieAuthentication")
            .AddCookie("CookieAuthentication", options =>
    {
        options.AccessDeniedPath = "/Login/Ops/";
        options.LoginPath = "/Login/Entrar";
    });
        //services.AddMvc();

        // Configurando o serviço de documentação do Swagger

        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        services.AddScoped<IReadRepository<Product>, ApplicationRepository<Product>>();
        services.AddScoped<IWriteRepository<Product>, ApplicationRepository<Product>>();
        services.AddScoped<IReadRepository<Sales>, ApplicationRepository<Sales>>();
        services.AddScoped<IWriteRepository<Sales>, ApplicationRepository<Sales>>();
        services.AddScoped<IReadRepository<Stock>, ApplicationRepository<Stock>>();
        services.AddScoped<IWriteRepository<Stock>, ApplicationRepository<Stock>>();
        services.AddScoped<IReadRepository<User>, ApplicationRepository<User>>();
        services.AddScoped<IWriteRepository<User>, ApplicationRepository<User>>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISalesService, SalesService>();
        services.AddScoped<IStockService, StockService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseAuthentication();

        app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

internal static class StartupExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDataContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
              sqlServerOptionsAction: sqlOptions =>
              {
                  sqlOptions.MigrationsAssembly(typeof(ApplicationDataContext).GetTypeInfo().Assembly.GetName().Name);
                  //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                  sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
              }));

        return services;
    }
}