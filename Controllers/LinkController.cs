using dotnet_url_shortner.Models;
using dotnet_url_shortner.Services;
using dotnet_url_shortner.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_url_shortner.Controllers;

[ApiController]
[Route("[controller]")]

public class LinkController : ControllerBase
{
    private LinkService _linkService;
    public LinkController(LinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet("{code}")]
    public ActionResult<string> Get(string? code) 
    {
        var link = _linkService.GetUrl(code);

        if (link == null) 
            return NotFound();

        return link;
    }

    [HttpPost]
    public IActionResult Create(Link link)
    {
        _linkService.Add(link);
        return CreatedAtAction(nameof(Get), new { id = link.Id }, link);
    }

    [HttpDelete("{code}")]
    public IActionResult Delete(Link link)
    {
        var testLink = _linkService.Get(link);

        if (testLink == null)
            return NotFound();

        _linkService.Delete(testLink);

        return NoContent();
    }
}