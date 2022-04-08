using AG.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace AG.Identity.TagHelpers
{
    [HtmlTargetElement("getUserInfo")]
    public class GetUserInfo : TagHelper
    {
        public int UserId { get; set; }
        private readonly UserManager<AppUser> _usermanager;

      public GetUserInfo(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string html = "";
            var user = await _usermanager.Users.SingleOrDefaultAsync(x => x.Id == UserId);
            var roles = await _usermanager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                html += role + "";
            }
            output.Content.SetHtmlContent(html);
        }

    }
}
