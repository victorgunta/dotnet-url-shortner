using dotnet_url_shortner.Models;
using dotnet_url_shortner.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_url_shortner.Services;

public class LinkService
{

    private readonly UrlShortnerDb _context;

    public LinkService(UrlShortnerDb context)
    {
        _context = context;
    }
    public void Add(Link link)
    {
        throw new NotImplementedException();
    }

    public Link Create(Link newLink)
    {
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

    public Link Get(Link link)
    {
        throw new NotImplementedException();
    }

    public void Delete(Link link)
    {
        throw new NotImplementedException();
    }

    public string GetUrl(string? code) 
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Link> GetUserLinks(User user)
    {
        return _context.Links
            .AsNoTracking()
            .ToList();
    }
}