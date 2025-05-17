using ApiRestFerreteria.Data;
using ApiRestFerreteria.Models;
using ApiRestFerreteria.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    public class UsuarioController : ApiController

    {
        public UsuarioController() : base() { }

        [HttpPost]
        [Route("rest/api/CrearUsuario")]
        public IHttpActionResult CrearUsuario(Models.Usuario model)
        {
            return Ok(new Data.Usuario().CrearUsuario(model));
        }

        [HttpGet]
        [Route("rest/api/ObtenerUsuarios")]
        public IHttpActionResult ObtenerUsuarios()
        {
            return Ok(new Data.Usuario().ObtenerUsuarios());
        }

        [HttpGet]
        [Route("rest/api/ObtenerUsuarioPorId/{id}")]
        public IHttpActionResult ObtenerUsuario(int id)
        {
            return Ok(new Data.Usuario().ObtenerUsuarioPorId(id));
        }

        [HttpPut]
        [Route("rest/api/ActualizarUsuario")]
        public IHttpActionResult ActualizarUsuario(Models.Usuario model)
        {
            return Ok(new Data.Usuario().ActualizarUsuario(model));
        }

        [HttpPut]
        [Route("rest/api/DesactivarUsuario")]
        public IHttpActionResult DesactivarUsuario(int id)
        {
            return Ok(new Data.Usuario().DesactivarUsuario(id));

        }

        [HttpDelete]
        [Route("rest/api/EliminarUsuario/{id}")]
        public IHttpActionResult EliminarUsuario(int id)
        {
            return Ok(new Data.Usuario().EliminarUsuario(id));
        }

        [HttpPost]
        [Route("rest/api/LoginUsuario")]
        public IHttpActionResult LoginUsuario([FromBody] dynamic loginData)
        {
            string username = loginData.Username;
            string userPassword = loginData.UserPassword;
            string codigoUsuario = loginData.CodigoUsuario;

            var response = new Data.Usuario().LoginUsuario(username, userPassword, codigoUsuario);
            return Ok(response);
        }



    }

}

