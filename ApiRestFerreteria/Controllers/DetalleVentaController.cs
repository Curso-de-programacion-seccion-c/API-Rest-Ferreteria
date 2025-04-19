using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    [RoutePrefix("api/DetalleVenta")]
    public class DetalleVentaController : ApiController
    {
        private readonly Data.DetalleVentaRepository _detalleVentaRepository = new Data.DetalleVentaRepository();

        [HttpGet]
        [Route("factura/{idFactura}")]
        public IHttpActionResult GetDetallesVenta(int idFactura)
        {
            var response = _detalleVentaRepository.getDetallesVenta(idFactura);

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    result = response.data
                });
            }

            if (response.data == null || response.data.Count == 0)
            {
                return NotFound();
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data
            });
        }

        [HttpGet]
        [Route("detalle/{idDetalle}")]
        public IHttpActionResult GetDetalleVenta(int idDetalle)
        {
            if (idDetalle <= 0)
            {
                return BadRequest("ID de detalle de venta no puede ser menor o igual a cero");
            }

            var response = _detalleVentaRepository.getDetalleVenta(idDetalle);

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    result = response.data
                });
            }

            if (response.data == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data
            });
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult insertarDetalle([FromBody] Models.DetalleVentaDB detalleVentaDB)
        {
            if (detalleVentaDB == null)
            {
                return BadRequest("El detalle de venta no puede ser nulo");
            }

            if (detalleVentaDB.IdFactura <= 0)
            {
                return BadRequest("El ID de la factura no puede ser menor o igual a cero");
            }
            if (detalleVentaDB.IdArticulo <= 0)
            {
                return BadRequest("El ID del artículo no puede ser menor o igual a cero");
            }

            if (detalleVentaDB.Cantidad <= 0)
            {
                return BadRequest("La cantidad no puede ser menor o igual a cero");
            }

            var response = _detalleVentaRepository.insertarDetalleVenta(detalleVentaDB.IdFactura, detalleVentaDB.IdArticulo, detalleVentaDB.Cantidad);

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data
            });
        }

        [HttpPut]
        [Route("{idDetalle}")]
        public IHttpActionResult UpdateDetalleVenta(int idDetalle, [FromBody] Models.DetalleVentaDB detalleVentaDB)
        {
            if (detalleVentaDB == null)
            {
                return BadRequest("El detalle de venta no puede ser nulo");
            }

            if (idDetalle <= 0)
            {
                return BadRequest("ID de detalle de venta no puede ser menor o igual a cero");
            }

            var response = _detalleVentaRepository.actualizarDetalle(idDetalle, detalleVentaDB.IdArticulo, detalleVentaDB.Cantidad);

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data
            });
        }

        [HttpDelete]
        [Route("{idDetalle}")]
        public IHttpActionResult DeleteDetalleVenta(int idDetalle)
        {
            if (idDetalle <= 0)
            {
                return BadRequest("ID de detalle de venta no puede ser menor o igual a cero");
            }

            var response = _detalleVentaRepository.eliminarDetalle(idDetalle);

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data
            });
        }
    }

}