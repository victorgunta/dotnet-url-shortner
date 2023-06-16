using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Data;

public class UrlShortnerDb : DbContext
{
    public UrlShortnerDb(DbContextOptions options) : base(options) {}
    public DbSet<Link> Links => Set<Link>();

    public DbSet<User> Users => Set<User>();
}