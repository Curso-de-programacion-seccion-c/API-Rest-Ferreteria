using System;

namespace ApiRestFerreteria.Cliente
{
    public class csEstructuraCliente
    {
        public class RequestCliente
        {
            public string Dpi { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NIT { get; set; }
            public string CorreoElectronico { get; set; }
            public string Telefono { get; set; }
            public DateTime FechaRegistro { get; set; }
        }

        public class ResponseCliente
        {
            public int Respuesta { get; set; }
            public string DescripcionRespuesta { get; set; }
        }

        public class ClienteData
        {
            public int IdCliente { get; set; }
            public string Dpi { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NIT { get; set; }
            public string CorreoElectronico { get; set; }
            public string Telefono { get; set; }
            public DateTime FechaRegistro { get; set; }
        }
    }
}