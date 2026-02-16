using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StockLens.data;
using StockLens.Models;
using StockLens.Repositories.Cities;
using StockLens.Repositories.Industries;
using StockLens.Repositories.Quotes;
using StockLens.Repositories.Sector;
using StockLens.Repositories.Tickers;
using StockLens.Services.Analytics.GeneralAnalytics;
using StockLens.Services.Analytics.Heatmap;
using StockLens.Services.Analytics.TopTen;
using StockLens.Services.Auth.AuthService;
using StockLens.Services.Auth.Token;
using StockLens.Services.FileReaderFacade;
using StockLens.Services.HttpRequester;
using StockLens.Services.HttpRequester.AnalyticsHttpRequester;
using StockLens.Services.HttpRequester.MoexHttpRequester;
using StockLens.Services.Industries;
using StockLens.Services.Moex;
using StockLens.Services.QuotesService;
using StockLens.Services.Sector;
using StockLens.Services.Tickers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<IIndustriesRepository, IndustriesRepository>();
builder.Services.AddScoped<IIndustriesService, IndustriesService>();
builder.Services.AddScoped<ITickersRepository, TickersRepository>();
builder.Services.AddScoped<ITickersService, TickersService>();
builder.Services.AddScoped<ICitiesRepositroy, CitiesRepository>();
builder.Services.AddScoped<IDataBaseFillingFacade, DataBaseFillingFacade>();
builder.Services.AddScoped<ICitiesRepositroy, CitiesRepository>();
builder.Services.AddScoped<IHttpRequester, MoexHttpRequester>();
builder.Services.AddScoped<IHttpRequester, AnalyticsHttpRequester>();
builder.Services.AddScoped<IMoexService, MoexService>();
builder.Services.AddScoped<IQuotesRepository, QuotesRepository>();
builder.Services.AddScoped<IQuotesService, QuotesService>();
builder.Services.AddScoped<IGeneralAnalyticsFacade, GeneralAnalyticsFacade>();
builder.Services.AddScoped<IHeatmapFacade, HeatmapFacade>();
builder.Services.AddScoped<ITopTenFacade, TopTenFacade>();

builder.Services.AddHttpClient<IHttpRequester, MoexHttpRequester>(client =>
{
    client.BaseAddress = new Uri("https://iss.moex.com/");
});

builder.Services.AddHttpClient<IHttpRequester, AnalyticsHttpRequester>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:8000/"); //TODO: —юда адрес питоновского сервера
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
);

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireUppercase = true; 
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };

 
});

builder.Services.AddScoped<ITokenCreator, TokenCreator>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

    option.SwaggerDoc("v1", new OpenApiInfo { Title = "StockLense API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
