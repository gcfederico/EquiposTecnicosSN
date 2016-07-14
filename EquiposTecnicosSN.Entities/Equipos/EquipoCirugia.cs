using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposCirugia")]
    public class EquipoCirugia : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Cirugía";
        }
    }
}