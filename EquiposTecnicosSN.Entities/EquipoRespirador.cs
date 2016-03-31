using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquiposTecnicosSN.Entities
{
    [Table("EquiposRespirador")]
    public class EquipoRespirador : EquipoBase
    {
        public EquipoRespirador() : base()
        {
            this.Tipo = TipoDeEquipo.Respirador;
        }
    }
}
