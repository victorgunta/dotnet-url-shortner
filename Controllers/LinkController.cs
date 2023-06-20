using Microsoft.AspNetCore.Mvc;
using dotnet_url_shortner.Models;
using dotnet_url_shortner.Services;

namespace dotnet_url_shortner.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LinkController : ControllerBase
{
    private readonly LinkService _linkService;

    public LinkController(LinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet("{code}")]
    public ActionResult<string> GetUrlByShortCode(string? code) 
    {
        var link = _linkService.GetUrlByShortCode(code);

        if (link == null)
            return NotFound();

        return link;
    }

    [HttpPost]
    public IActionResult Create(string url)
    {
        var newLink = _linkService.Create(url);
        return CreatedAtAction(nameof(GetUrlByShortCode), new { code = newLink.ShortCode }, newLink);
    }

    [HttpDelete("{code}")]
    public IActionResult Delete(Link link)
    {
        var testLink = _linkService.GetById(link.Id);

        if (testLink == null)
            return NotFound();

        _linkService.DeleteById(testLink.Id);

        return NoContent();
    }
}