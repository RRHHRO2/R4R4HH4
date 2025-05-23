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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public int MunicipioExpedicion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int LugarNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int EPS { get; set; }
        public int FondoPension { get; set; }
        public int FondoCesantias { get; set; }
        public int IdAreaTrabajo { get; set; }
        public int IdContrato { get; set; }

        // Propiedades de navegación
        public virtual Municipio MunicipioNacimiento { get; set; }
        public virtual Municipio MunicipioExpedicionDoc { get; set; }
        public virtual SeguridadSocial EntidadEPS { get; set; }
        public virtual SeguridadSocial EntidadFondoPension { get; set; }
        public virtual SeguridadSocial EntidadFondoCesantias { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual AreaTrabajo AreaTrabajo { get; set; }

        // Relaciones inversas
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Ausencia> Ausencias { get; set; }
        public virtual ICollection<EmpleadoDependiente> Dependientes { get; set; }
    }

}