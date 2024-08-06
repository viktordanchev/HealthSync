using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace HealthSync.Server.Controllers
{
    [Route("api/аuthentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            return BadRequest();
        }
    }
}
