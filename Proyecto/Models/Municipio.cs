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
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int IdDepartamento { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Empleado> EmpleadosLugarNacimiento { get; set; }
        public virtual ICollection<Empleado> EmpleadosMunicipioExpedicion { get; set; }
    }
}