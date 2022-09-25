using JWT.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT.Database
{
    public class DemoContext:IdentityDbContext<ApplicationUser>
    {
        public DemoContext(DbContextOptions<DemoContext> opt):base(opt)
        {

        }
    }
}
