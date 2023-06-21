using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Data;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Pages.Links
{
    public class IndexModel : PageModel
    {
        private readonly dotnet_url_shortner.Data.UrlShortnerDb _context;

        public IndexModel(dotnet_url_shortner.Data.UrlShortnerDb context)
        {
            _context = context;
        }

        public IList<Link> Link { get;set; }

        public async Task OnGetAsync()
        {
            Link = await _context.Links.ToListAsync();
        }
    }
}
