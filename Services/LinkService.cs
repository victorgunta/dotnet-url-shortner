using System.Text;
using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Models;
using dotnet_url_shortner.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace dotnet_url_shortner.Services;

public class LinkService
{

    private readonly UrlShortnerDb _dbContext;
    private readonly StatService _statService;
    private readonly Random _random = new Random();

    public LinkService(UrlShortnerDb context, StatService statService)
    {
        _dbContext = context;
        _statService = statService;
    }

    public Link Create(string newUrl)
    {
        // TODO check if we have a valid user

        var newLink = new Link {
            ShortCode = RandomString(6, false),
            Url = newUrl,
            LastUpdate = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        _dbContext.Links.Add(newLink);
        _dbContext.SaveChanges();

        return newLink;
    }

    public Link? GetById(int id)
    {
        return _dbContext.Links
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public void DeleteById(int id)
    {
        var link = _dbContext.Links.Find(id);
        if (link is not null)
        {
            _dbContext.Links.Remove(link);
            _dbContext.SaveChanges();
        }        
    }

    public string? GetUrlByShortCode(string? shortCode) 
    { 
        var link = _dbContext.Links
            .SingleOrDefault(p => p.ShortCode == shortCode);

        if (link == null) {
            return "Not found";
        } else {
            // First, check expiration
            if (link.Expires < DateTime.UtcNow) 
                return "Expired";

            // If we have a valid request we increment the hits and update the link
            link.Hits++;
            link.LastUpdate = DateTime.UtcNow;

            var newStat = _statService.Create(link);

            //_dbContext.Stats.Add(newStat);
            _dbContext.Links.Update(link);
            _dbContext.SaveChanges();
            return link.Url;
        }
    }

    public IEnumerable<Link> GetUserLinks(User user)
    {
        return _dbContext.Links
            .AsNoTracking()
            .ToList();
    }

    private string RandomString(int size, bool lowerCase = false)
    {
      var builder = new StringBuilder(size);

      // Unicode/ASCII Letters are divided into two blocks
      // (Letters 65–90 / 97–122):
      // The first group containing the uppercase letters and
      // the second group containing the lowercase.

      // char is a single Unicode character
      char offset = lowerCase ? 'a' : 'A';
      const int lettersOffset = 26; // A...Z or a..z: length = 26

      for (var i = 0; i < size; i++)
      {
        var @char = (char)_random.Next(offset, offset + lettersOffset);
        builder.Append(@char);
      }

      return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }
}