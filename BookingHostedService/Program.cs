using Azure;
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
using WhereToServices.HttpMessageHandlers;
using WhereToServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<WhereTo_BookingQueueSubscriber>();
builder.Services.AddScoped<IUnitOfWork, WhereTo_BookingUnitOfWork>();
builder.Services.AddScoped<IQueueMessageSubscriber<WhereToBookingMessage>, WhereTo_BookingQueueMessageSubscriberService>();
builder.Services.AddScoped<IEventPublisherService<BookingFinishedEvent>, EventPublisherService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<HttpMessageHandler, BookingService_HttpMessageHandler>();
builder.Services.AddHttpClient<WhereTo_BookingQueueSubscriber>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5269/");
}).AddHttpMessageHandler<BookingService_HttpMessageHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DataBase configuration
builder.Services.AddDbContext<WhereTo_BookingDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

builder.Services.AddAzureClients(b =>
{
    b.AddQueueServiceClient((builder.Configuration.GetConnectionString("AzureStorage")));

    Uri endpoint = new Uri(builder.Configuration["EventGrid:EventGridTopicEndpoint"]);
    AzureKeyCredential credential = new AzureKeyCredential(builder.Configuration["EventGrid:EventGridTopicKey"]);
    b.AddEventGridPublisherClient(endpoint, credential);
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
