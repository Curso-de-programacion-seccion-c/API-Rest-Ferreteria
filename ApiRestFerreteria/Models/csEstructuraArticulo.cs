using System.Collections.Generic;
using System.Data;

namespace ApiRestFerreteria.Articulo
{
    public class csEstructuraArticulo
    {
        public class requestArticulo
        {
            public string NombreArticulo { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
            public int IdCategoria { get; set; }
            public int IdProveedor { get; set; }
        }

        public class responseArticulo
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }

        public class articulo
        {
            public int IdArticulo { get; set; }
            public string NombreArticulo { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
        }

        public class responseAllArticulos
        {
            public List<articulo> articulos { get; set; }
            public DataTable listaArticulos { get; set; }
        }
    }
}
