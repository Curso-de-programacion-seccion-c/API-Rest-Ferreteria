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


    }
}