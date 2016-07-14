using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposLuces")]
    public class EquipoLuces : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Luces";
        }
    }
}