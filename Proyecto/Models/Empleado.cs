using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int IdMunicipio { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdAreaTrabajo { get; set; }

        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; set; }

        [ForeignKey("IdTipoDocumento")]
        public virtual TipoDocumento TipoDocumento { get; set; }

        [ForeignKey("IdAreaTrabajo")]
        public virtual AreaTrabajo AreaTrabajo { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Dependiente> Dependientes { get; set; }
        public virtual ICollection<Ausencia> Ausencias { get; set; }
    }
}