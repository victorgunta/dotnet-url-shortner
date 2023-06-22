using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using dotnet_url_shortner.Authorization;
using dotnet_url_shortner.Services;
using dotnet_url_shortner.Data;

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

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UrlShortnerDb>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
        
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<LinkService>();

// Authorization handlers.
builder.Services.AddScoped<IAuthorizationHandler, ContactIsOwnerAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, LinkAdministratorsAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, LinkManagerAuthorizationHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UrlShortnerDb>();
    context.Database.Migrate();
    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    // dotnet user-secrets set SeedUserPW <pw>

    var testUserPw = builder.Configuration.GetValue<string>("SeedUserPW");

   await SeedData.Initialize(services, testUserPw);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
