using dotnet_url_shortner.Models;
using dotnet_url_shortner.Services;
using dotnet_url_shortner.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UrlShortner") ?? "Data Source=UlrShortner.db";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<UrlShortnerDb>(connectionString);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LinkService>();

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
