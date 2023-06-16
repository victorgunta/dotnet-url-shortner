using Microsoft.EntityFrameworkCore;

namespace dotnet_url_shortner.Models;

public class Link {
    public int Id { get; set; }

    public string? ShortCode { get; set; }

    public string? Url { get; set; }

    public int Hits { get; set; }

    public bool Active { get; set; }

    public User? User { get; set; }
}