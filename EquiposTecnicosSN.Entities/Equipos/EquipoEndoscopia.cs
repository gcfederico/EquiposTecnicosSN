using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposEndoscopia")]
    public class EquipoEndoscopia : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Endoscopía";
        }
    }
}
