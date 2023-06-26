using Domain.RepositoryInterfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Controllers;
using Presentation.Validators;
using Services.Abstractions.Dtos;
using Services.Interfaces;
using Services.Services;
using Swashbuckle.AspNetCore.Filters;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
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

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BusStopsController).Assembly);

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.ApiName = "publictransitapi";
        options.Authority = "https://localhost:5443";
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IValidator<BusStopAddDto>, BusStopAddDtoValidator>();
builder.Services.AddScoped<IValidator<BusStopUpdateDto>, BusStopUpdateDtoValidator>();

builder.Services.AddDbContextPool<DataContext>(contextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    contextOptionsBuilder.UseNpgsql(connectionString, builder =>
    {
        builder.UseNetTopologySuite();
        builder.MigrationsAssembly("Web");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
