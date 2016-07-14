using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposOtros")]
    public class EquipoOtro : Equipo
    {
        public override string Tipo()
        {
            return "Otros";
        }
    }
}
