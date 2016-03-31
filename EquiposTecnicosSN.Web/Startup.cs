using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquiposTecnicosSN.Web.Startup))]
namespace EquiposTecnicosSN.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
