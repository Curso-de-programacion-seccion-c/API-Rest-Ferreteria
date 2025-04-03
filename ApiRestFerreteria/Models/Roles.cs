using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
    public class Roles
    {
        [Key]
        public byte IdRol { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal Sueldo { get; set; }
    }
}
