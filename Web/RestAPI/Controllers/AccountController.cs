using Core.Models.Account;
using RestAPI.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Common.Errors;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HealthSync.Server.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;
        private ITokenService _tokenService;
        private IEmailSender _emailSender;
        private IMemoryCache _cache;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            ITokenService tokenService,
            IEmailSender emailSender,
            IMemoryCache cache,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _cache = cache;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

                return BadRequest(ModelState);
            }

            var verificationCode = Guid.NewGuid().ToString().Substring(0, 6);
            _cache.Set(user.Email, verificationCode, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            var subject = "Confirm your registration!";
            var message = $"Your verification code: <strong>{verificationCode}</strong>";
            await _emailSender.SendEmailAsync(user.Email, subject, message);

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

                return BadRequest(ModelState);
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userLogin.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return BadRequest(ModelState);
            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user.Id);
            var accessTokenExpireTime = _tokenService.GetTokenExpireTime(accessToken);
            _tokenService.AppendTokenToCookie(HttpContext, "accessToken", accessToken, accessTokenExpireTime);

            if (userLogin.RememberMe)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
                var refreshTokenExpireTime = _tokenService.GetTokenExpireTime(refreshToken);
                _tokenService.AppendTokenToCookie(HttpContext, "refreshToken", refreshToken, refreshTokenExpireTime);
            }

            return Ok();
        }

        [HttpPost("confirmRegistration")]
        public async Task<IActionResult> ConfirmRegistration(string userEmail, string verificationCode)
        {
            var code = _cache.Get(userEmail);

            if (code != null && code == verificationCode)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                user!.EmailConfirmed = true;

                return Ok();
            }

            return BadRequest(new { Error = "Invalid verification code!" });
        }

        [HttpPost("resendVerificationCode")]
        public async Task<IActionResult> ResendVerificationCode(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var verificationCode = Guid.NewGuid().ToString().Substring(0, 6);
            _cache.Set(user.Email, verificationCode, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            var subject = "Confirm your registration!";
            var message = $"Your verification code: <strong>{verificationCode}</strong>";
            await _emailSender.SendEmailAsync(user.Email, subject, message);

            return Ok(new { Message = "New verification code is sended." });
        }

        /// <summary>
        /// This check if there is valid access token.
        /// </summary>
        [HttpGet("isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            Request.Cookies.TryGetValue("accessToken", out var token);

            return Ok(new { IsAuthenticated = token != null });
        }

        /// <summary>
        /// This method return current user name.
        /// </summary>
        [HttpGet("getUserName")]
        public IActionResult GetUserName()
        {
            return Ok(new { UserName = User.Identity.Name });
        }
    }
}
