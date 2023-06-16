using Domain.RepositoryInterfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;
using Presentation.Validators;
using Services.Abstractions.Dtos;
using Services.Interfaces;
using Services.Services;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BusStopsController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
