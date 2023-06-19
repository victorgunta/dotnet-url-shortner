using dotnet_url_shortner.Models;
using dotnet_url_shortner.Services;
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
    public IActionResult Create(string Url, int? userId)
    {
        var link = _linkService.Create(Url, userId);
        return CreatedAtAction(nameof(Get), new { id = link.Id }, link);
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