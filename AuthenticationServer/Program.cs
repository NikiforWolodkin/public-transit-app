using AuthenticationServer;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System.Reflection;
using Serilog.Sinks.SystemConsole.Themes;
using AuthenticationServer.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
    {
        opt.MigrationsAssembly(migrationsAssembly);
    }));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = contextBuilder =>
        {
            contextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.MigrationsAssembly(migrationsAssembly);
            });
        };
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = contextBuilder =>
        {
            contextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.MigrationsAssembly(migrationsAssembly);
            });
        };
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();



Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

var seed = args.Contains("/seed");

if (true)
{
    Log.Information("Seeding database...");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    SeedData.EnsureSeedData(connectionString);
    Log.Information("Done seeding database.");
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
