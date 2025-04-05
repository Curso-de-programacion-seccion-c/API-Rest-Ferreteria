namespace ApiRestFerreteria.Rol
{
    public class csEstructuraRoles
    {

        public class requestRol
        {
            public string Nombre { get; set; }
            public decimal Sueldo { get; set; }
        }


        public class responseRol
        {
            public int respuesta { get; set; }
            public string descripcion_respuesta { get; set; }
        }


        public class RolData
        {
            public int IdRol { get; set; }
            public string Nombre { get; set; }
            public decimal Sueldo { get; set; }
        }
    }
}