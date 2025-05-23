using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TipoDocumento
    {
        [Key]
        public int IdTipoDocumento { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreTipo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}