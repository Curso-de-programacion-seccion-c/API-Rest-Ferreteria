using ApiRestFerreteria.Models;
using System.Linq;
using System.Web.Http;

namespace ApiRestFerreteria.Controllers
{
    public class RolesController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: api/Roles
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            try
            {
                var roles = _context.Roles.ToList();
                return Ok(roles);
            }
            catch (System.Exception err)
            {
                throw err;
            }
        }
    }
}