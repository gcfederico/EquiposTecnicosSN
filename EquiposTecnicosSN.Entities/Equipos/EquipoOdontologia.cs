using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposOdontologia")]
    public class EquipoOdontologia : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Odontología";
        }
    }
}