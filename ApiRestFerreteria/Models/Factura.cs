using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRestFerreteria.Models
{
    public class Factura
    {
        public int idFactura { get; set; }
        public int idEmpleado { get; set; }
        public int idCliente { get; set; }
        public DateTime fecha { get; set; }
        public decimal Total_Pago { get; set; }
        public byte idFormaPago { get; set; }
    }

    public class ListarFacturas
    {
        public int idFactura { get; set; }
        public DateTime fecha { get; set; }
        public decimal Total_Pago { get; set; }
        public int idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public byte idFormaPago { get; set; }
        public string nombreFormaPago { get; set; }
    }
}