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
    public class DeleteModel : PageModel
    {
        private readonly dotnet_url_shortner.Data.UrlShortnerDb _context;

        public DeleteModel(dotnet_url_shortner.Data.UrlShortnerDb context)
        {
            _context = context;
        }

        [BindProperty]
        public Link Link { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Links.FirstOrDefaultAsync(m => m.Id == id);

            if (Link == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Links.FindAsync(id);

            if (Link != null)
            {
                _context.Links.Remove(Link);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
