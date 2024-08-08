using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Account;
using static Common.Errors;

namespace HealthSync.Server.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("register");
            }

            var user = await _userManager.FindByEmailAsync(userRegister.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", UsedEmail);

                return RedirectToAction("register");
            }

            user = new ApplicationUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                //UCN = userRegister.UCN,
                //PhoneNumber = userRegister.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return RedirectToAction("register");
            }

            return Ok();
        }
    }
}
