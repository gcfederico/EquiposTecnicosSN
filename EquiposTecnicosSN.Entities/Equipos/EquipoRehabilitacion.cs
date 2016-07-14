using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposRehabilitacion")]
    public class EquipoRehabilitacion : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Rehabilitación";
        }
    }
}