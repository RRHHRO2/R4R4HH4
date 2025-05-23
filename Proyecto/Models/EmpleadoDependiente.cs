using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class EmpleadoDependiente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string TipoDependiente { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}