using dotnet_url_shortner.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_url_shortner.Data
{
    public class Metadata : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MetadataDb");
        }
        public DbSet<Stat> Stats => Set<Stat>();
    }
}