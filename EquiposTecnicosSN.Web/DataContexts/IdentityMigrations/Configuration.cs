namespace EquiposTecnicosSN.Web.DataContexts.IdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    internal sealed class Configuration : DbMigrationsConfiguration<EquiposTecnicosSN.Web.DataContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\IdentityMigrations";
        }

        protected override void Seed(EquiposTecnicosSN.Web.DataContexts.IdentityDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };

            if (context.Roles.Any(r => r.Name == "admin"))
            {
                roleManager.Update(adminRole);
            }
            else
            {
                roleManager.Create(adminRole);
            }


            if (context.Roles.Any(r => r.Name == "user"))
            {
                roleManager.Update(userRole);
            }
            else
            {
                roleManager.Create(userRole);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var adminUser = new ApplicationUser { UserName = "admin@neuquenET.com", Email = "admin@nequenET.com" };
            var testUser = new ApplicationUser { UserName = "usuario@neuquenET.com", Email = "usuario@nequenET.com" };



            if (!context.Users.Any(u => u.UserName == "admin@neuquenET.com"))
            {
                userManager.Create(adminUser, "Admin@2016");
                userManager.AddToRole(adminUser.Id, adminRole.Name);
            }

            if (!context.Users.Any(u => u.UserName == "usuario@neuquenET.com"))
            {
                userManager.Create(testUser, "Usuario@2016");
                userManager.AddToRole(testUser.Id, userRole.Name);
            }

        }
    }
}
