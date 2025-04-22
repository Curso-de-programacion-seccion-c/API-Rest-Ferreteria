using System.Web.Http;
using ApiRestFerreteria.Articulo;

namespace ApiRestFerreteria.Controllers
{
    public class ArticulosController : ApiController
    {
        private csArticulo articuloService = new csArticulo();

        [HttpPost]
        [Route("rest/api/crearArticulo")]
        public IHttpActionResult crearArticulo([FromBody] csEstructuraArticulo.requestArticulo model)
        {
            var response = articuloService.insertarArticulo(model.NombreArticulo, model.Precio, model.Stock, model.IdCategoria, model.IdProveedor);
            if (response.respuesta == 0)
            {
                return BadRequest(response.descripcion_respuesta);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("rest/api/obtenerArticulos")]
        public IHttpActionResult obtenerArticulos()
        {
            var response = articuloService.obtenerArticulos();
            if (response.respuesta == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("rest/api/actualizarArticulo/{codeArticulo}")]
        public IHttpActionResult actualizarArticulo(string codeArticulo, [FromBody] csEstructuraArticulo.requestArticulo model)
        {
            if (short.TryParse(codeArticulo, out short codigo))
            {
                var response = articuloService.actualizarArticulo(codigo, model.NombreArticulo, model.Precio, model.Stock);
                if (response.respuesta == 0)
                {
                    return BadRequest(response.descripcion_respuesta);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest("El CodeArticulo debe ser un número entero válido.");
            }
        }

        [HttpDelete]
        [Route("rest/api/eliminarArticulo/{codeArticulo}")]
        public IHttpActionResult eliminarArticulo(string codeArticulo)
        {
            if (short.TryParse(codeArticulo, out short codigo))
            {
                var response = articuloService.eliminarArticulo(codigo);
                if (response.respuesta == 0)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            else
            {
                return BadRequest("El CodeArticulo debe ser un número entero válido.");
            }
        }

    }
}
