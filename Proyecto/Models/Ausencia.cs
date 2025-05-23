using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Ausencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdEmpleado { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Tipo de Ausencia")]
        [Required]
        public string TipoAusencia { get; set; }

        [Display(Name = "Justificación")]
        [Required]
        public string Justificacion { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}
