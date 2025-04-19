using ApiRestFerreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    public class EmpleadoController : ApiController
    {

        private readonly Data.EmpleadoRepository _repo = new Data.EmpleadoRepository();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleados")]
        public IHttpActionResult GetEmpleados()
        {
            var empleados = _repo.GetEmpleado();

            if (empleados == null)
            {
                return NotFound();
            }

            if (empleados.StatusCode != 200)
            {
                return Content((HttpStatusCode)empleados.StatusCode, new
                {
                    status = empleados.StatusCode,
                    message = empleados.Message,
                    result = empleados.data
                });
            }

            return Ok(new
            {
                status = empleados.StatusCode,
                message = empleados.Message,
                result = empleados.data,
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleados/{id}")]
        public IHttpActionResult GetEmpleado(byte id)
        {
            var response = _repo.GetEmpleado(id);

            if (response == null)
            {
                return NotFound();
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    data = response.data,
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                data = response.data,
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/empleados")]
        public IHttpActionResult PostEmpleado([FromBody] EmpleadoDB empleado)
        {
            if (empleado == null)
            {
                return BadRequest("Información incompleta");
            }

            // Validaciones básicas
            if (string.IsNullOrEmpty(empleado.Dpi) || string.IsNullOrEmpty(empleado.Nombre))
            {
                return BadRequest("DPI y Nombre son campos requeridos");
            }

            var response = _repo.CrearEmpleado(empleado);

            if (response == null)
            {
                return BadRequest("Error al crear empleado");
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    data = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                data = response.data
            });
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/empleados/{id}")]
        public IHttpActionResult PutEmpleado(byte id, [FromBody] EmpleadoDB empleado)
        {
            if (empleado == null)
            {
                return BadRequest("Información incompleta");
            }

            if (string.IsNullOrEmpty(empleado.Dpi) || string.IsNullOrEmpty(empleado.Nombre))
            {
                return BadRequest("DPI y Nombre son campos requeridos");
            }

            empleado.idEmpleado = id;

            var response = _repo.ActualizarEmpleado(empleado);

            if (response == null)
            {
                return BadRequest("Error al actualizar empleado");
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    data = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                data = response.data
            });
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/empleados/{id}")]
        public IHttpActionResult DeleteEmpleado(byte id)
        {
            var response = _repo.EliminarEmpleado(id);

            if (response == null)
            {
                return BadRequest("Error al eliminar empleado");
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    data = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                data = response.data
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empleados/buscar")]
        public IHttpActionResult BuscarEmpleados([FromUri] string filtro)
        {
            var response = _repo.BuscarEmpleados(filtro);

            if (response == null)
            {
                return NotFound();
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    data = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                data = response.data
            });
        }
    }

}
