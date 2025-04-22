using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
	public class FormaPagoController : ApiController
    {
        private readonly Data.FormaPagoRepository _repo = new Data.FormaPagoRepository();

        [HttpGet]
        [System.Web.Http.Route("api/FormaPago")] 

        public  IHttpActionResult ObtenerFormaPagos()
        {
            var FormaPago = _repo.GetFormaPago();
            if(FormaPago.StatusCode != 200)
            {
                return Content((HttpStatusCode)FormaPago.StatusCode, new
                {
                    status = FormaPago.StatusCode,
                    message = FormaPago.Message,
                    result = FormaPago.data
                });
            }

            return Ok(new
            {
                status = FormaPago.StatusCode,
                message = FormaPago.Message,
                result = FormaPago.data
            });
        }
        [HttpGet]
        [Route("api/formaspago/{id}")]
        public IHttpActionResult ObtenerFormaPagoPorId(byte id)
        {
            var resultado = _repo.ObtenerPorId(id);
            if (resultado.StatusCode != 200)
            {
                return Content((HttpStatusCode)resultado.StatusCode, new
                {
                    status = resultado.StatusCode,
                    message = resultado.Message,
                    result = resultado.data
                });
            }

            return Ok(new
            {
                status = resultado.StatusCode,
                message = resultado.Message,
                result = resultado.data
            });
        }

        [HttpPost]
        [Route("api/formaspago")]
        public IHttpActionResult CrearFormaPago([FromBody] FormasPagoDB formaPago)
        {
            var resultado = _repo.Crear(formaPago);
            if (resultado.StatusCode != 200)
            {
                return Content((HttpStatusCode)resultado.StatusCode, new
                {
                    status = resultado.StatusCode,
                    message = resultado.Message,
                    result = resultado.data
                });
            }

            return Ok(new
            {
                status = resultado.StatusCode,
                message = resultado.Message,
                result = resultado.data
            });
        }

        [HttpPut]
        [Route("api/formaspago/{id}")]
        public IHttpActionResult ActualizarFormaPago(byte id, [FromBody] FormasPagoDB formaPago)
        {
            formaPago.idFormaPago = id;
            var resultado = _repo.Actualizar(formaPago);
            if (resultado.StatusCode != 200)
            {
                return Content((HttpStatusCode)resultado.StatusCode, new
                {
                    status = resultado.StatusCode,
                    message = resultado.Message,
                    result = resultado.data
                });
            }

            return Ok(new
            {
                status = resultado.StatusCode,
                message = resultado.Message,
                result = resultado.data
            });
        }

        [HttpDelete]
        [Route("api/formaspago/{id}")]
        public IHttpActionResult EliminarFormaPago(byte id)
        {
            var resultado = _repo.Eliminar(id);
            if (resultado.StatusCode != 200)
            {
                return Content((HttpStatusCode)resultado.StatusCode, new
                {
                    status = resultado.StatusCode,
                    message = resultado.Message,
                    result = resultado.data
                });
            }

            return Ok(new
            {
                status = resultado.StatusCode,
                message = resultado.Message,
                result = resultado.data
            });
        }  
    }
}