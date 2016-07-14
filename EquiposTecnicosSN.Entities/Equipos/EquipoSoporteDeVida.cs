using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposSoporteDeVida")]
    public class EquipoSoporteDeVida : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Soporte de Vida";
        }
    }
}
