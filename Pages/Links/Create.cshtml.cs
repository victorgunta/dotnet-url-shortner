using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using dotnet_url_shortner.Authorization;
using dotnet_url_shortner.Data;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Pages.Links
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(
            UrlShortnerDb context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Link? Link { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Link.UserId = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Link, LinkOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Links.Add(Link);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
