using dotnet_url_shortner.Middleware;
using dotnet_url_shortner.Services;
using dotnet_url_shortner.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UrlShortner") ?? "Data Source=UlrShortner.db";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<UrlShortnerDb>(connectionString);
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<StatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.SaveUserMetadata();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => @"Basic API only. Navigate to /swagger to open the Swagger test UI.");

app.Run();
