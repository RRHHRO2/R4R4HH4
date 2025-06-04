using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required]
        public int IdTipoDocumento { get; set; }

        [Display(Name = "Numero de Documento")]
        [Required]
        public string NumeroDocumento { get; set; }

        [Display(Name = "Fecha de Expedición")]
        [Required]
        public DateTime FechaExpedicion { get; set; }

        [Display(Name = "Lugar de Expedición")]
        [Required]
        public int MunicipioExpedicion { get; set; }

        [Display(Name = "Nombres")]
        [Required]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        [Required]
        public string Apellidos { get; set; }

        [Display(Name = "Lugar de Nacimiento")]
        [Required]
        public int LugarNacimiento { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        public string Direccion { get; set; }

        [Display(Name = "Barrio")]
        [Required]
        public string Barrio { get; set; }

        [Display(Name = "Teléfono")]
        [Required]
        [StringLength(10)]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string Telefono { get; set; }

        [Display(Name = "Celular")]
        [Required]
        [StringLength(10)]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string Celular { get; set; }

        [Display(Name = "Correo")]
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "EPS")]
        [Required]
        public int EPS { get; set; }

        [Display(Name = "Fondo de Pensión")]
        [Required]
        public int FondoPension { get; set; }

        [Display(Name = "Fondo de Cesantias")]
        [Required]
        public int FondoCesantias { get; set; }

        [Display(Name = "Area de Trabajo")]
        [Required]
        public int IdAreaTrabajo { get; set; }

        // Nueva propiedad para guardar el PDF
        public byte[] ArchivoPDF { get; set; }

        // Propiedades de navegación
        public virtual Municipio MunicipioNacimiento { get; set; }
        public virtual Municipio MunicipioExpedicionDoc { get; set; }
        public virtual SeguridadSocial SSEPS { get; set; }
        public virtual SeguridadSocial SSAFP { get; set; }
        public virtual SeguridadSocial SSAFC { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual AreaTrabajo AreaTrabajo { get; set; }

        // Relaciones inversas
        public virtual ICollection<Ausencia> Ausencias { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<EmpleadoDependiente> Dependientes { get; set; }
    }
}
