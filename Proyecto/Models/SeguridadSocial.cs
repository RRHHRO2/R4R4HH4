using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class SeguridadSocial
    {
        [Key]
        public int IdSeguridadSocial { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreEntidad { get; set; }

        [Required]
        [StringLength(100)]
        public string Tipo { get; set; } // Ej: EPS, ARL, etc.
    }
}