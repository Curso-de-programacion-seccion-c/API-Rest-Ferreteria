using System.Web.Http;
using ApiRestFerreteria.Rol;

namespace ApiRestFerreteria.Controllers
{
    public class RolesController : ApiController
    {
        public RolesController() : base() { }

        [HttpPost]
        [Route("rest/api/insertarRol")]
        public IHttpActionResult insertarRol(csEstructuraRol.requestRol model)
        {
            return Ok(new csRol().insertarRol(model.Nombre, model.Sueldo));
        }

        [HttpGet]
        [Route("rest/api/obtenerRoles")]
        public IHttpActionResult obtenerRoles()
        {
            return Ok(new csRol().obtenerRoles());
        }

        [HttpGet]
        [Route("rest/api/obtenerRol/{id}")]
        public IHttpActionResult obtenerRol(int id)
        {
            return Ok(new csRol().obtenerRolPorId(id));
        }

        [HttpPut]
        [Route("rest/api/actualizarRol/{id}")]
        public IHttpActionResult actualizarRol(int id, csEstructuraRol.requestRol model)
        {
            return Ok(new csRol().actualizarRol(id, model.Nombre, model.Sueldo));
        }

        [HttpDelete]
        [Route("rest/api/eliminarRol/{id}")]
        public IHttpActionResult eliminarRol(int id)
        {
            return Ok(new csRol().eliminarRol(id));
        }
    }
}
