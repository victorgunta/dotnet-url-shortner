using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace dotnet_url_shortner.Models;

public class User 
{
    public int Id { get; set; }
    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool Active { get; set; }
    
    [JsonIgnore]
    public ICollection<Link>? Links { get; set; }
}