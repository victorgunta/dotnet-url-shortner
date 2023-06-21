using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Models;
using dotnet_url_shortner.Data;

namespace dotnet_url_shortner.Services;

public class StatService
{

    private readonly UrlShortnerDb _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StatService(UrlShortnerDb context, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Stat Create(Link link)
    {
        var context = _httpContextAccessor.HttpContext;
        var newStat = new Stat {
            Link = link,            
            Timestamp = DateTime.UtcNow,
            RequestData = context?.Request.Headers["Browser"].ToString()
        };

        _dbContext.Stats.Add(newStat);
        _dbContext.SaveChanges();

        return newStat;
    }

    public IEnumerable<Stat> GetStats(Link link)
    {
        return _dbContext.Stats
            .AsNoTracking()
            .ToList();
    }
}