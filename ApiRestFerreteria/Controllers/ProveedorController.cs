using System;
using System.Net;
using System.Web.Http;
using ApiRestFerreteria.Proveedor;

namespace ApiRestFerreteria.Controllers
{
    public class ProveedoresController : ApiController
    {
        private csProveedor proveedorService = new csProveedor();

        // Crear proveedor
        [HttpPost]
        [Route("rest/api/crearProveedor")]
        public IHttpActionResult crearProveedor([FromBody] Models.Proveedor model)
        {
            if (model == null)
            {
                return BadRequest("Informacion incompleta");
            }

            var response = proveedorService.CrearProveedor(model);

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
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data,
            });
        }

        // Obtener proveedores
        [HttpGet]
        [Route("rest/api/obtenerProveedores")]
        public IHttpActionResult obtenerProveedores()
        {
            var response = proveedorService.obtenerProveedores();

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
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data,
            });
        }

        // Obtener proveedor por ID
        [HttpGet]
        [Route("rest/api/obtenerProveedor/{id}")]
        public IHttpActionResult obtenerProveedor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID invalido");
            }

            var response = proveedorService.obtenerProveedor(id);

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
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data,
            });
        }

        // Actualizar proveedor
        [HttpPut]
        [Route("rest/api/actualizarProveedor/{id}")]
        public IHttpActionResult actualizarProveedor(int id, [FromBody] Models.Proveedor model)
        {
            if (model == null)
            {
                return BadRequest("Informacion incompleta");
            }

            if (id <= 0)
            {
                return BadRequest("ID invalido");
            }

            var modelo = new Models.Proveedor
            {
                IdProveedor = Convert.ToByte(id),
                NombreProveedor = model.NombreProveedor,
                Telefono = model.Telefono,
                NombreContacto = model.NombreContacto
            };

            var response = proveedorService.ActualizarProveedor(modelo);

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
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data,
            });
        }

        // Eliminar proveedor
        [HttpDelete]
        [Route("rest/api/eliminarProveedor/{id}")]
        public IHttpActionResult eliminarProveedor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID invalido");
            }

            var response = proveedorService.EliminarProveedor(id);

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
                    result = response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                result = response.data,
            });
        }
    }
}