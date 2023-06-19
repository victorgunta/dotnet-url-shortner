using System.Text;
using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Models;
using dotnet_url_shortner.Data;

namespace dotnet_url_shortner.Services;

public class LinkService
{

    private readonly UrlShortnerDb _context;
    private readonly Random _random = new Random();

    public LinkService(UrlShortnerDb context)
    {
        _context = context;
    }

    public Link Create(string newUrl)
    {
        // TODO check if we have a valid user

        var newLink = new Link {
            ShortCode = RandomString(6, false),
            Url = newUrl,
            Active = true
        };

        _context.Links.Add(newLink);
        _context.SaveChanges();

        return newLink;
    }

    public Link? GetById(int id)
    {
        return _context.Links
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public void DeleteById(int id)
    {
        var link = _context.Links.Find(id);
        if (link is not null)
        {
            _context.Links.Remove(link);
            _context.SaveChanges();
        }        
    }

    public string? GetUrlByShortCode(string? shortCode) 
    { 
        var link = _context.Links
            .AsNoTracking()
            .SingleOrDefault(p => p.ShortCode == shortCode);

        if (link == null) {
            return "";
        } else {
            // If we have a valid request we increment the hits and update the link
            link.Hits++;
            _context.Links.Update(link);
            _context.SaveChanges();
            return link.Url;
        }
        
    }

    public IEnumerable<Link> GetUserLinks(User user)
    {
        return _context.Links
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