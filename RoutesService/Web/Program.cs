using Domain.Entities;
using Domain.Services;
using Domain.Services.RepositoryInterfaces;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Services.Interfaces;
using Services.Services;
using Services.Validators;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TransitApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:5443/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"publictransitapi.scope", "publictransitapi - full access"}
                }
            }
        }
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.ApiName = "publictransitapi";
        options.Authority = "https://auth:5443";
    });

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IValidator<List<RouteStop>>, RouteStopValidator>();

builder.Services.AddScoped<IBusStopRepository, BusStopRepository>();
builder.Services.AddScoped<IRouteStopRepository, RouteStopRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRouteService, RouteService>();

builder.Services.AddDbContextPool<DataContext>(contextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    contextOptionsBuilder
        .UseLazyLoadingProxies()
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
        {
            builder.MigrationsAssembly("Web");
        });
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.SetInMemorySagaRepositoryProvider();

    var assembly = Assembly.GetEntryAssembly();

    cfg.AddConsumers(assembly);
    cfg.AddSagaStateMachines(assembly);
    cfg.AddSagas(assembly);
    cfg.AddActivities(assembly);

    cfg.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("rabbitmq", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("m2m.client");
        options.OAuthClientSecret("SuperSecretPassword");
        options.OAuthScopes("publictransitapi.scope");
    });
}

app.UseHttpsRedirection();

app.UseExceptionHandlingMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();