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
            var response = articuloService.insertarArticulo(model.CodeArticulo,
                model.NombreArticulo, model.Precio, model.Stock, model.Descripcion, model.IdCategoria, model.IdProveedor);

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
           
            return Ok(response);
        }
        [HttpGet]
        [Route("rest/api/obtenerArticuloPorId/{id}")]
        public IHttpActionResult obtenerArticuloPorId(int id)
        {
            var articulo = articuloService.obtenerArticuloPorId(id);

            if (articulo == null || articulo.IdArticulo == 0)
            {
                return NotFound();
            }

            return Ok(articulo);
        }
        [HttpPut]
        [Route("rest/api/actualizarArticulo/{idArticulo}")]
        public IHttpActionResult actualizarArticulo(string idArticulo, [FromBody] csEstructuraArticulo.requestArticulo model)
        {
            if (short.TryParse(idArticulo, out short art))
            {
                var response = articuloService.actualizarArticulo(
                    art,
                    model.CodeArticulo,
                    model.NombreArticulo,
                    model.Precio,
                    model.Descripcion,
                    model.Stock,
                    model.IdCategoria,
                    model.IdProveedor
                );
                if (response.respuesta == 0)
                {
                    return BadRequest(response.descripcion_respuesta);
                }
                return Ok(response.descripcion_respuesta);
            }
            else
            {
                return BadRequest("Por favor, vuelva a intentarlo con un número válido");
            }
        }


        [HttpDelete]
        [Route("rest/api/eliminarArticulo/{id}")]
        public IHttpActionResult eliminarArticulo(int id)
        {
            var response = articuloService.eliminarArticulo(id);
            return Ok(response);
        }


    }
}
