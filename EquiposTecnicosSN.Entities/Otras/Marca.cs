﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquiposTecnicosSN.Entities.Otras
{
    [Table("Marcas")]
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int FabricanteId { get; set; }

        [ForeignKey("FabricanteId")]
        public virtual Fabricante Fabricante { get; set; }

        public virtual ICollection<Modelo> Modelos { get; set; }
    }
}