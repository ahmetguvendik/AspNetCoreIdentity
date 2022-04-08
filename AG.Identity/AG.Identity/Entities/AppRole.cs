using Microsoft.AspNetCore.Identity;

namespace AG.Identity.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
