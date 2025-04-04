using ApiRestFerreteria.Models;
using System.Linq;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    public class RolesController : ApiController
    {

        private readonly Data.RolesRepository _repo = new Data.RolesRepository();

        // GET: api/Roles
        [HttpGet]
        [Route("api/roles")]
        public IHttpActionResult GetRoles()
        {
            var roles = _repo.GetRoles();

            if (roles == null || !roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }
    }
}