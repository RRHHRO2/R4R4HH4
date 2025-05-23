using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class SeguridadSocial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSeguridadSocial { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Empleado> EmpleadosEPS { get; set; }
        public virtual ICollection<Empleado> EmpleadosFondoPension { get; set; }
        public virtual ICollection<Empleado> EmpleadosFondoCesantias { get; set; }
    }
}