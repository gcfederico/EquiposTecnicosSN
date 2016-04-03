using EquiposTecnicosSN.Entities;
using System.Data.Entity;

namespace EquiposTecnicosSN.Web.DataContexts
{
    public class UsuarioTecnicoDb : DbContext
    {
        public DbSet<UsuarioTecnico> UsuariosTecnicos { get; set; }

        public DbSet<Ubicacion> Ubicaciones { get; set; }
    }
}