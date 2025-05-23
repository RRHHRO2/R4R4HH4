using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreDepartamento { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}