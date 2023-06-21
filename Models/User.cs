using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnet_url_shortner.Models;

public class User 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool Active { get; set; }
    
    [JsonIgnore]
    public ICollection<Link>? Links { get; set; }
}