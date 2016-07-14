using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposMonitoreo")]
    public class EquipoMonitoreo : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Monitoreo";
        }
    }
}