using Azure.Storage.Blobs;
using Domain.ServiceRepositories;
using Infrastructure.StorageServices;
using Services.Interfaces;
using Services.Services;
using TransitApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddSingleton(provider => 
    {
        var connectionString = builder.Configuration.GetValue<string>("AzureBlobStorage:ConnectionString");

        return new BlobServiceClient(connectionString);
    });

builder.Services.AddSingleton<IBlobService, BlobService>();

builder.Services.AddScoped<IImageService, ImageService>();

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
