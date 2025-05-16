using ApiRestFerreteria.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace ApiRestFerreteria.Controllers
{
    [System.Web.Http.RoutePrefix("api/Reportes")]
    public class ReportesController : ApiController
    {
        private readonly Data.ReporteRepository _repo = new Data.ReporteRepository();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("FacturaDetalle/{idFactura:int}")]
        public IHttpActionResult GetFacturaById(int idFactura)
        {
            var respose = _repo.getFacturaById(idFactura);

            if(respose.StatusCode != 200)
            {
                return Content((HttpStatusCode)respose.StatusCode, new
                {
                    status = respose.StatusCode,
                    message = respose.Message,
                    result = respose.data
                });
            }

            return Ok(new
            {
                status = respose.StatusCode,
                message = respose.Message,
                result = respose.data
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("FacturaPorFecha")]
        public IHttpActionResult GetFacturaByFecha([FromBody] Models.ReportesFacturas.RequestReporteFecha requestReporteFecha)
        {
            if (requestReporteFecha == null)
            {
                return BadRequest("Informacion incompleta");
            }

            var respose = _repo.getFacturaByFecha(requestReporteFecha);

            if (respose.StatusCode != 200)
            {
                return Content((HttpStatusCode)respose.StatusCode, new
                {
                    status = respose.StatusCode,
                    message = respose.Message,
                    result = respose.data
                });
            }

            return Ok(new
            {
                status = respose.StatusCode,
                message = respose.Message,
                result = respose.data
            });
        }
    }
}