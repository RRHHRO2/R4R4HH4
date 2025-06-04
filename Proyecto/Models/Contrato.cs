using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Contrato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Tipo de Contrato")]
        [Required]
        public int IdTipoContrato { get; set; }

        [Display(Name = "Empleado")]
        [Required]
        public int IdEmpleado { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Terminación")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Archivo PDF del Contrato")]
        public byte[] ArchivoPDF { get; set; }

        // Propiedades de navegación
        public virtual TipoContrato TipoContrato { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
