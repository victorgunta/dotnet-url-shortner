using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_url_shortner.Authorization;
using dotnet_url_shortner.Data;
using dotnet_url_shortner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_url_shortner.Pages.Links
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(
            UrlShortnerDb context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Link Link { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await Context.Links.FirstOrDefaultAsync(m => m.Id == id);

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

            Link = await Context.Links.FindAsync(id);

            if (Link != null)
            {
                Context.Links.Remove(Link);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
