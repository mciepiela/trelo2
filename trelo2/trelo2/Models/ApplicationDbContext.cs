using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace trelo2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           // this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Task> Tasks { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}