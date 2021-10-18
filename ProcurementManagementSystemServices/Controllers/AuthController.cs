using Microsoft.AspNetCore.Mvc;
using ProcurementManagementSystemData.Models;
using ProcurementManagmentSystemAPIs.Models;

namespace ProcurementManagementSystemData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ProcurementManagmentContext _context;

        public AuthController(ProcurementManagmentContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet("Authenticate")]
        public ActionResult Authenticate(string username, string password)
        {
            User user = _context.Users.Find(username);
            if (user.PasswordHash.Equals(password))
            {
                AuthResult authResult = new AuthResult();
                authResult.Username = user.Username;
                authResult.FirstName = user.FirstName;
                authResult.LastName = user.LastName;
                authResult.Token = "ABC123";

                return Ok(authResult);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
