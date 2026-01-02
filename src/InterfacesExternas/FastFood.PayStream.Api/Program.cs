using Microsoft.EntityFrameworkCore;
using FastFood.PayStream.Infra.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Obter connection string do appsettings.json
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(dbConnectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' não foi encontrada no appsettings.json.");
}

// Registrar DbContext com PostgreSQL
builder.Services.AddDbContext<PayStreamDbContext>(options =>
    options.UseNpgsql(dbConnectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
