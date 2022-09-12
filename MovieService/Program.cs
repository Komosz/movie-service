using Microsoft.OpenApi.Models;
using MovieService.Data;
using MovieService.Factories;
using MovieService.Repositories;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new StringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Service", Version = "v1" });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSingleton<CsvContext>();
builder.Services.AddSingleton<SqliteDbContext>();
builder.Services.AddScoped<IMovieRepository, CsvMovieRepository>();
builder.Services.AddScoped<IMovieRepository, SqliteMovieRepository>();
builder.Services.AddScoped<IMovieRepositoryFactory, MovieRepositoryFactory>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

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
