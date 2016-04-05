using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities
{
    [Table("EquiposCirugia")]
    public class EquipoCirugia : Equipo
    {
        public EquipoCirugia() : base()
        {
            this.InformacionComercial = new InformacionComercial();
        }
    }
}
