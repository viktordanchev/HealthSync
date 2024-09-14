using Core.Models.Account;
using Core.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ITokenService tokenService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
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

                return BadRequest(ModelState);
            }

            user = new ApplicationUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return Unauthorized(ModelState);
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userLogin.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return Unauthorized(ModelState);
            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user.Id);
            AppendTokenToCookie("accessToken", accessToken);

            if (userLogin.RememberMe)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
                AppendTokenToCookie("refreshToken", refreshToken);
            }

            return Ok(new { redirectTo = "/home" });
        }

        [HttpGet("isAccessTokenExpired")]
        public IActionResult IsAccessTokenExpired()
        {
            Request.Cookies.TryGetValue("accessToken", out var token);

            return Ok(new { IsExpired = token == null });
        }

        /// <summary>
        /// This method appends JWT token to cookie.
        /// </summary>
        private void AppendTokenToCookie(string type, string token)
        {
            var expTime = _tokenService.GetTokenExpireTime(token);

            HttpContext.Response.Cookies.Append(type, token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = expTime,
                });
        }
    }
}
