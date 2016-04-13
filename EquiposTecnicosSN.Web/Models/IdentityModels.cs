using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EquiposTecnicosSN.Entities;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
           //userIdentity.AddClaim(new Claim("UbicacionId", this.UbicacionId.ToString()));

            return userIdentity;
        }

        public int UbicacionId { get; set; }

        public string confirmationToken { get; set; }
    }

    /*public static class IdentityExtensions
    {
        public static string GetOrganizationId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UbicacionId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }*/
}