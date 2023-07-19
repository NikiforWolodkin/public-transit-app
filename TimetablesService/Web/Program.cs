using Domain.RepositoryInterfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MassTransit;
using Serilog;
using Services.Interfaces;
using Services.Services;
using System.Reflection;
using TransitApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

builder.Services.Configure<TimetableDbSettings>(builder.Configuration.GetSection("TimetableDb"));

builder.Services.AddSingleton<TimetableRepository>();

builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();

builder.Services.AddScoped<ITimetableService, TimetableService>();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
