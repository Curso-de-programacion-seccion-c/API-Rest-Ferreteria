using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
    public class Roles
    {
        public byte IdRol { get; set; }

        public string Nombre { get; set; }

        public decimal Sueldo { get; set; }
    }
}
