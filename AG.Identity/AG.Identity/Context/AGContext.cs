using AG.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AG.Identity.Context
{
    public class AGContext :IdentityDbContext<AppUser,AppRole,int>
    {
        public AGContext(DbContextOptions<AGContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().Property(x=>x.Gender).IsRequired(false);
            builder.Entity<AppUser>().Property(x=>x.ImagePath).IsRequired(false);
            base.OnModelCreating(builder);
        }
    }
}
