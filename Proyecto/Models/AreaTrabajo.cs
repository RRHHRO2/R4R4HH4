using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class AreaTrabajo
    {
        [Key]
        public int IdAreaTrabajo { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreArea { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
