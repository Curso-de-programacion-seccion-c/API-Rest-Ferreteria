using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
	public class EmpleadoDB
	{
		public int idEmpleado;
		public string Dpi;
		public string Nombre;
		public string Apellido;
		public string Puesto;
		public string CorreoElectronico;
		public string Telefono;
        public string FechaContratacion { get; set; }
        public byte IdRol { get; set; }
        public string nombreRol { get; set; }
        public decimal Sueldo { get; set; }
    }

	public class CrearEmpleado
	{
        public string Dpi { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Puesto { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string FechaContratacion { get; set; }
        public byte IdRol { get; set; }
    }
}