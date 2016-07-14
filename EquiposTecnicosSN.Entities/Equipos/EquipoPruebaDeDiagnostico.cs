using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Equipos
{
    [Table("EquiposPruebasDeDiagnostico")]
    public class EquipoPruebaDeDiagnostico : Equipo
    {
        public override string Tipo()
        {
            return "Equipo de Prueba de Diágnostico";
        }
    }
}