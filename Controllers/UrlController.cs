using Microsoft.AspNetCore.Mvc;
using dotnet_url_shortner.Services;

namespace dotnet_url_shortner.Controllers;

[Route("")]

public class UrlController : ControllerBase
{
    private readonly LinkService _linkService;

    public UrlController(LinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet("{code}")]
    public ActionResult GetUrl(string? code) 
    {
        var link = _linkService.GetUrlByShortCode(code);

        if (link == "")
            return NotFound();
        else
            return Redirect(link);
    }
}