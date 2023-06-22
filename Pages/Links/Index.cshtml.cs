using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using dotnet_url_shortner.Authorization;
using dotnet_url_shortner.Data;
using dotnet_url_shortner.Models;

namespace dotnet_url_shortner.Pages.Links
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(
            UrlShortnerDb context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Link> Links { get; set; }

        public async Task OnGetAsync()
        {
            var links = from c in Context.Links
                select c;

            // First check if the user is Administrator or Manager
            var isAuthorized = User.IsInRole(Constants.LinkManagersRole) || User.IsInRole(Constants.LinkAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved links are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                links = links.Where(c => c.Active || c.UserId == currentUserId);
            }

            Links = await links.ToListAsync();
        }
    }
}
