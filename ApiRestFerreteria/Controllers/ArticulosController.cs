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
            return Ok(articuloService.insertarArticulo(model.NombreArticulo, model.Precio, model.Stock, model.IdCategoria, model.IdProveedor));
        }

        [HttpGet]
        [Route("rest/api/obtenerArticulos")]
        public IHttpActionResult obtenerArticulos()
        {
            return Ok(articuloService.obtenerArticulos());
        }

        [HttpPut]
        [Route("rest/api/actualizarArticulo/{id}")]
        public IHttpActionResult actualizarArticulo(int id, [FromBody] csEstructuraArticulo.requestArticulo model)
        {
            return Ok(articuloService.actualizarArticulo(id, model.NombreArticulo, model.Precio, model.Stock));
        }

        [HttpDelete]
        [Route("rest/api/eliminarArticulo/{id}")]
        public IHttpActionResult eliminarArticulo(int id)
        {
            return Ok(articuloService.eliminarArticulo(id));
        }
    }
}
