using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using trelo2.Models;

namespace trelo2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<trelo2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(trelo2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AddUsers(context);
        }

        void AddUsers(ApplicationDbContext context)
        {
            var user = new ApplicationUser{UserName = "user1@mail.com"};
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.Create(user, "password");
        }
    }
}
