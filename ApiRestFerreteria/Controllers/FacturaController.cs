using ApiRestFerreteria.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    public class FacturaController : ApiController
    {
        private readonly FacturaRepository _facturaRepository = new FacturaRepository();

        [HttpGet]
        [Route("api/facturas")]
        public IHttpActionResult GetFacturas()
        {
            var facturas = _facturaRepository.getFacturas();

            if(facturas == null)
            {
                return NotFound();
            }

            if(facturas.StatusCode != 200)
            {
                return Content((HttpStatusCode)facturas.StatusCode, new
                {
                    status = facturas.StatusCode,
                    message = facturas.Message,
                    result = facturas.data
                });
            }

            return Ok(new
            {
                status = facturas.StatusCode,
                message = facturas.Message,
                result = facturas.data,
            });
        }

        [HttpGet]
        [Route("api/factura/{id}")]
        public IHttpActionResult GetFactura(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de factura no puede ser menor o igual a cero");
            }

            var factura = _facturaRepository.getFactura(id);

            if (factura == null)
            {
                return NotFound();
            }

            if (factura.StatusCode != 200)
            {
                return Content((HttpStatusCode)factura.StatusCode, new
                {
                    status = factura.StatusCode,
                    message = factura.Message,
                    result = factura.data
                });
            }

            return Ok(new
            {
                status = factura.StatusCode,
                message = factura.Message,
                result = factura.data,
            });
        }

        [HttpPost]
        [Route("api/factura")]
        public IHttpActionResult PostFactura([FromBody] Models.Factura factura)
        {
            if (factura == null)
            {
                return BadRequest("Factura no puede ser nula");
            }

            var result = _facturaRepository.crearFactura(factura);

            if (result.StatusCode != 200)
            {
                return Content((HttpStatusCode)result.StatusCode, new
                {
                    status = result.StatusCode,
                    message = result.Message,
                    result = result.data
                });
            }

            return Ok(new
            {
                status = result.StatusCode,
                message = result.Message,
                result = result.data,
            });
        }

        [HttpPut]
        [Route("api/factura/{id}")]
        public IHttpActionResult PutFactura(int id, [FromBody] Models.Factura factura)
        {
            if (factura == null)
            {
                return BadRequest("Factura no puede ser nula");
            }

            if (id <= 0)
            {
                return BadRequest("ID de factura no puede ser menor o igual a cero");
            }

            factura.idFactura = id;

            var result = _facturaRepository.actualizarFactura(factura);

            if (result.StatusCode != 200)
            {
                return Content((HttpStatusCode)result.StatusCode, new
                {
                    status = result.StatusCode,
                    message = result.Message,
                    result = result.data
                });
            }

            return Ok(new
            {
                status = result.StatusCode,
                message = result.Message,
                result = result.data,
            });
        }

        [HttpDelete]
        [Route("api/factura/{id}")]
        public IHttpActionResult DeleteFactura(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de factura no puede ser menor o igual a cero");
            }

            var result = _facturaRepository.eliminarFactura(id);

            if (result.StatusCode != 200)
            {
                return Content((HttpStatusCode)result.StatusCode, new
                {
                    status = result.StatusCode,
                    message = result.Message,
                    result = result.data
                });
            }

            return Ok(new
            {
                status = result.StatusCode,
                message = result.Message,
                result = result.data,
            });
        }
    }
}