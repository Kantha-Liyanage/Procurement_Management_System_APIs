using Microsoft.AspNetCore.Mvc;
using ProcurementManagementSystemData.DTO;
using ProcurementManagementSystemServices.Models;
using ProcurementManagementSystemServices.Providers;

namespace ProcurementManagementSystemData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
        private readonly ProcurementManagmentContext context;

        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager, ProcurementManagmentContext context)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            this.context = context;
        }

        // GET: api/Addresses
        [HttpGet("Authenticate")]
        public ActionResult Authenticate(string username, string password)
        {
            User user = this.context.Users.Find(username);
            if (user == null) {
                return NotFound();
            }

            if (user.PasswordHash.Equals(password))
            {
                //JWT Payload
                AuthResult authResult = new AuthResult();
                authResult.Username = user.Username;
                authResult.FirstName = user.FirstName;
                authResult.LastName = user.LastName;

                //Role
                UserType userType = this.context.UserTypes.Find(user.UserType);

                //Token
                authResult.Token = jWTAuthenticationManager.Authenticate(user.Username, userType.Name);

                return Ok(authResult);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
