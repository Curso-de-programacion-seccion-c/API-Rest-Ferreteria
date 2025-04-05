using System.Web.Http;
using ApiRestFerreteria.Proveedor;

namespace ApiRestFerreteria.Controllers
{
    public class ProveedoresController : ApiController
    {
        private csProveedor proveedorService = new csProveedor();

        // Crear proveedor
        [HttpPost]
        [Route("rest/api/crearProveedor")]
        public IHttpActionResult crearProveedor([FromBody] ProveedorRequest model)
        {
            return Ok(proveedorService.insertarProveedor(model.NombreProveedor, model.Telefono, model.NombreContacto));
        }

        // Obtener proveedores
        [HttpGet]
        [Route("rest/api/obtenerProveedores")]
        public IHttpActionResult obtenerProveedores()
        {
            return Ok(proveedorService.obtenerProveedores());
        }

        // Actualizar proveedor
        [HttpPut]
        [Route("rest/api/actualizarProveedor/{id}")]
        public IHttpActionResult actualizarProveedor(int id, [FromBody] ProveedorRequest model)
        {
            return Ok(proveedorService.actualizarProveedor(id, model.NombreProveedor, model.Telefono, model.NombreContacto));
        }

        // Eliminar proveedor
        [HttpDelete]
        [Route("rest/api/eliminarProveedor/{id}")]
        public IHttpActionResult eliminarProveedor(int id)
        {
            return Ok(proveedorService.eliminarProveedor(id));
        }

        // Activar o desactivar proveedor
        [HttpPut]
        [Route("rest/api/activarDesactivarProveedor/{id}")]
        public IHttpActionResult activarDesactivarProveedor(int id, [FromBody] bool isActive)
        {
            return Ok(proveedorService.activarDesactivarProveedor(id, isActive));
        }
    }

    public class ProveedorRequest
    {
        public string NombreProveedor { get; set; }
        public string Telefono { get; set; }
        public string NombreContacto { get; set; }
    }
}