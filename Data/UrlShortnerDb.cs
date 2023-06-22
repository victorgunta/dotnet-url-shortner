using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Data;

public class UrlShortnerDb : IdentityDbContext
{
    public UrlShortnerDb(DbContextOptions options) : base(options) {}
    public DbSet<Link> Links => Set<Link>();
}