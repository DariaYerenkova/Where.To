using Microsoft.EntityFrameworkCore;
using WhereToDataAccess;
using WhereToDataAccess.Interfaces;
using WhereToServices;
using WhereToServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>(e => new UnitOfWork(e.GetService<WhereToDataContext>()));
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DataBase configuration
builder.Services.AddDbContext<WhereToDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"), b => b.MigrationsAssembly("WhereTo")));

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<WhereToDataContext>();
//    dbContext.EnsureCreated();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
