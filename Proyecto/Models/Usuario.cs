using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Numero de Documento")]
        [Required]
        public int Cedula { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required]
        public string NombreCompleto { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        public string Contrasena { get; set; }

        public byte[] HashKey { get; set; }
        public byte[] HashIV { get; set; }

        [Display(Name = "Rol")]
        [Required]
        public int IdRol { get; set; }

        [Display(Name = "Area")]
        [Required]
        public int IdArea { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string NuevaContrasena { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual AreaTrabajo AreaTrabajo { get; set; }
    }
}
