using System.Web.Http;
using ApiRestFerreteria.Cliente;

namespace ApiRestFerreteria.Controllers
{
    public class ClientesController : ApiController
    {
        public ClientesController() : base() { }

        [HttpPost]
        [Route("rest/api/insertarCliente")]
        public IHttpActionResult InsertarCliente(csEstructuraCliente.RequestCliente model)
        {
            return Ok(new csCliente().InsertarCliente(
                model.Dpi, model.Nombre, model.Apellido,
                model.NIT, model.CorreoElectronico,
                model.Telefono, model.FechaRegistro));
        }

        [HttpGet]
        [Route("rest/api/obtenerClientes")]
        public IHttpActionResult ObtenerClientes()
        {
            return Ok(new csCliente().ObtenerClientes());
        }
      
        [HttpGet]
        [Route("rest/api/obtenerCliente/{id}")]
        public IHttpActionResult ObtenerCliente(int id)
        {
            return Ok(new csCliente().ObtenerClientePorId(id));
        }
        
        [HttpPut]
        [Route("rest/api/actualizarCliente/{id}")]
        public IHttpActionResult ActualizarCliente(int id, csEstructuraCliente.RequestCliente model)
        {
            return Ok(new csCliente().ActualizarCliente(
                id, model.Dpi, model.Nombre, model.Apellido,
                model.NIT, model.CorreoElectronico,
                model.Telefono, model.FechaRegistro));
        }

        [HttpDelete]
        [Route("rest/api/eliminarCliente/{id}")]
        public IHttpActionResult EliminarCliente(int id)
        {
            return Ok(new csCliente().EliminarCliente(id));
        }
    }
}