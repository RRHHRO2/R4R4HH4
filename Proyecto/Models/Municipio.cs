using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Municipio
    {
        [Key]
        public int IdMunicipio { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreMunicipio { get; set; }

        public int IdDepartamento { get; set; }

        [ForeignKey("IdDepartamento")]
        public virtual Departamento Departamento { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}