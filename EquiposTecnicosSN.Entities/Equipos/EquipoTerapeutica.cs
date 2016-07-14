using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposTerapeutica")]
    public class EquipoTerapeutica : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Terapéutica";
        }
    }
}