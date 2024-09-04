using Core.Models.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Common.Errors;

namespace HealthSync.Server.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
                PhoneNumber = userRegister.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("login");
            }

            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return NotFound("nema");
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return NotFound("login");
            }

            var token = GenerateJWT(user);

            return Ok(token);
        }

        private string GenerateJWT(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
