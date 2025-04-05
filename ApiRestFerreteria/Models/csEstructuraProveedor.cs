using System.Collections.Generic;
using System.Data;

namespace ApiRestFerreteria.Proveedor
{
    public class csEstructuraProveedor
    {
        public class requestProveedor
        {
            public string NombreProveedor { get; set; }
            public string Telefono { get; set; }
            public string NombreContacto { get; set; }
            public bool IsActive { get; set; }
        }

        public class responseProveedor
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class proveedor
        {
            public int IdProveedor { get; set; }
            public string NombreProveedor { get; set; }
            public string Telefono { get; set; }
            public string NombreContacto { get; set; }
            public bool IsActive { get; set; }
        }

        // Estructura para devolver todos los proveedores
        public class responseAllProveedores
        {
            public List<proveedor> proveedores { get; set; }
            public DataTable listaProveedores { get; set; }

        }
    }
}