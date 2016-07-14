using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposInformatica")]
    public class EquipoInformatica : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Informática";
        }
    }
}