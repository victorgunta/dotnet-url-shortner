using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_url_shortner.Models;

public class Stat 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Link? Link { get; set; }

    public DateTime Timestamp { get; set; }

    public string? RequestData { get; set; }
}