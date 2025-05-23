using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TipoContrato
    {
        public int IdTipoContrato { get; set; }
        public string NombreTipoContrato { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}