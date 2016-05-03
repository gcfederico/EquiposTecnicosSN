using EquiposTecnicosSN.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace EquiposTecnicosSN.Web.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("EquiposTecnicosDbContext", throwIfV1Schema: false)
        {
        }

        static IdentityDb()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            //Database.SetInitializer<IdentityDb>(new IdentityDbInitializer());
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}