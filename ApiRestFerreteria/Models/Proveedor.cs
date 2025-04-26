using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
    public class Proveedor
    {
        public byte IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Telefono { get; set; }
        public string NombreContacto { get; set; }
    }
}