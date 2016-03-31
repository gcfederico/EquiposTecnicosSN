using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities
{
    [Table("EquiposClimatizacion")]
    public class EquipoClimatizacion : EquipoBase
    {
        public EquipoClimatizacion() : base()
        {
            this.Tipo = TipoDeEquipo.Climatizacion;
        }
    }
}