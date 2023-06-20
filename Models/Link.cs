using System.ComponentModel;

namespace dotnet_url_shortner.Models;

public class Link {
    public int Id { get; set; }

    public string? ShortCode { get; set; }

    public string? Url { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime Expires { get; set; }

    [DefaultValue(0)]
    public int Hits { get; set; }

    [DefaultValue("true")]
    public bool Active { get; set; }
    [DefaultValue(null)]
    public User? User { get; set; }
}