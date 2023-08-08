using Domain.RepositoryInterfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Validators;
using Services.Abstractions.Dtos;
using Services.Interfaces;
using Services.Services;
using Swashbuckle.AspNetCore.Filters;
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
        options.Authority = "https://auth:443";
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

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
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
