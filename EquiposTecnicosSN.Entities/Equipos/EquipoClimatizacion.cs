using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposClimatizacion")]
    public class EquipoClimatizacion : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Climatización";
        }
    }
}