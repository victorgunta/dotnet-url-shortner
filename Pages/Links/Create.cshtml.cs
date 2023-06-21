using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using dotnet_url_shortner.Data;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Pages.Links
{
    public class CreateModel : PageModel
    {
        private readonly dotnet_url_shortner.Data.UrlShortnerDb _context;

        public CreateModel(dotnet_url_shortner.Data.UrlShortnerDb context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Link Link { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Links.Add(Link);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
