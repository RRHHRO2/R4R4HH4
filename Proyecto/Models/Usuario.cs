using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public int? Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public int? IdRol { get; set; }
        public virtual Rol Rol { get; set; }

        public int? IdArea { get; set; }
        public virtual AreaTrabajo AreaTrabajo { get; set; }

        [HiddenInput]
        public byte[] HashKey { get; set; }

        [HiddenInput]
        public byte[] HashIV { get; set; }
    }
}
