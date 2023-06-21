using dotnet_url_shortner.Models;
using dotnet_url_shortner.Services;
using dotnet_url_shortner.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Select the connection string based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<UrlShortnerDb>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("UrlShortnerDatabase_Development")));
}
else
{
    builder.Services.AddDbContext<UrlShortnerDb>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("UrlShortnerDatabase_Production")));
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LinkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
