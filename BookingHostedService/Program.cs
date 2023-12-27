using Azure.Storage.Queues;
using BookingHostedService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System.Net.Http;
using WhereToDataAccess;
using WhereToDataAccess.Migrations;
using WhereToDataAccess.WhereTo_BookingInterfaces;
using WhereToServices;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<WhereTo_BookingQueueSubscriber>();
builder.Services.AddScoped<IUnitOfWork, WhereTo_BookingUnitOfWork>();
builder.Services.AddScoped<IQueueMessageSubscriber<WhereToBookingMessage>, WhereTo_BookingQueueMessageSubscriberService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<WhereTo_BookingQueueSubscriber>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5269/");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DataBase configuration
builder.Services.AddDbContext<WhereTo_BookingDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

builder.Services.AddAzureClients(b =>
{
    b.AddClient<QueueClient, QueueClientOptions>((_, _, _) =>
    {
        string storageConnectionString = builder.Configuration.GetConnectionString("AzureStorage");
        string queueName = builder.Configuration["QueueNames:WhereToBookingQueue"];
        return new QueueClient(storageConnectionString, queueName);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
