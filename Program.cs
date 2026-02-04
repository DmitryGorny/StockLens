using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Repositories.Industries;
using StockLens.Repositories.Sector;
using StockLens.Repositories.Tickers;
using StockLens.Services.Industries;
using StockLens.Services.Sector;
using StockLens.Services.Tickers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IIndustriesRepository, IndustriesRepository>();
builder.Services.AddScoped<IIndustriesService, IndustriesService>();
builder.Services.AddScoped<ITickersRepository, TickersRepository>();
builder.Services.AddScoped<ITickersService, TickersService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
);

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
