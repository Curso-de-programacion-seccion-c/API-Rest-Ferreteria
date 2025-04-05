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
    public class CategoriaController : ApiController
    {
        private readonly Data.CategoriaRepository _repo = new Data.CategoriaRepository();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/categorias")]
        public IHttpActionResult GetCategorias()
        {
            var categorias = _repo.GetCategorias();

            if (categorias == null)
            {
                return NotFound();
            }

            if (categorias.StatusCode != 200)
            {
                return Content((HttpStatusCode)categorias.StatusCode, new
                {
                    status = categorias.StatusCode,
                    message = categorias.Message,
                    result = categorias.data
                });
            }

            return Ok(new
            {
                status = categorias.StatusCode,
                message = categorias.Message,
                result = categorias.data,
            });
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/findCategoria")]
        public IHttpActionResult GetCategoria([FromBody] Models.Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Informacion incompleta");
            }

            var response = _repo.GetCategoria(categoria);

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
                    response.data,
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                response.data,
            });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/categorias")]
        public IHttpActionResult PostCategoria([FromBody] Models.Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Informacion incompleta");
            }

            if (string.IsNullOrEmpty(categoria.nombreCategoria))
            {
                return BadRequest("El nombre de categoria es requerido");
            }

            var response = _repo.CrearCategoria(categoria);

            if (response == null)
            {
                return BadRequest("Failed to create category.");
            }

            if(response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                response.data
            });
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/categorias/{id}")]
        public IHttpActionResult PutCategoria(int id, [FromBody] Models.Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Informacion incompleta");
            }

            if (string.IsNullOrEmpty(categoria.nombreCategoria))
            {
                return BadRequest("El nombre de categoria es requerido");
            }

            categoria.idCategoria = (byte)id;

            var response = _repo.ActualizarCategoria(categoria);

            if (response == null)
            {
                return BadRequest("Fallo al actualizacion");
            }

            if(response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                response.data
            });
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/categorias/{id}")]
        public IHttpActionResult DeleteCategoria(int id)
        {
            var response = _repo.EliminarCategoria(id);

            if (response == null)
            {
                return BadRequest("Fallo al eliminar la categoria");
            }

            if (response.StatusCode != 200)
            {
                return Content((HttpStatusCode)response.StatusCode, new
                {
                    status = response.StatusCode,
                    message = response.Message,
                    response.data
                });
            }

            return Ok(new
            {
                status = response.StatusCode,
                message = response.Message,
                response.data
            });
        }
    }
}