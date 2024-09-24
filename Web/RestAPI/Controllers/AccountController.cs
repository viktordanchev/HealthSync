using RestAPI.DTOs.Account;
using RestAPI.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Common.Errors.Account;
using static Common.Messages.Account;

namespace HealthSync.Server.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;
        private IJWTTokenService _tokenService;
        private IEmailSender _emailSender;
        private IMemoryCacheService _memoryCacheService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            IJWTTokenService tokenService,
            IEmailSender emailSender,
            IMemoryCacheService memoryCacheService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return BadRequest(new { NotVerified = true });
                }

                ModelState.AddModelError("Email", UsedEmail);

                return BadRequest(ModelState);
            }

            user = new ApplicationUser()
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            _memoryCacheService.Add(user.Email, token, TimeSpan.FromMinutes(1));
            await _emailSender.SendVrfCode(user.Email, token);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return BadRequest(ModelState);
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", InvalidLoginData);

                return BadRequest(ModelState);
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest(new { NotVerified = true });
            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user.Id);
            var accessTokenExpireTime = _tokenService.GetTokenExpireTime(accessToken);
            _tokenService.AppendTokenToCookie(HttpContext, "accessToken", accessToken, accessTokenExpireTime);

            if (request.RememberMe)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
                var refreshTokenExpireTime = _tokenService.GetTokenExpireTime(refreshToken);
                _tokenService.AppendTokenToCookie(HttpContext, "refreshToken", refreshToken, refreshTokenExpireTime);
            }

            return Ok();
        }

        [HttpPost("verifyAccount")]
        public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountRequest request)
        {
            if (_memoryCacheService.GetValue(request.Email).ToLower() == request.VrfCode.ToLower())
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                user!.EmailConfirmed = true;
                await _userManager.UpdateAsync(user!);

                return Ok();
            }

            return BadRequest(new { Error = InvalidVrfCode });
        }

        [HttpPost("resendVrfCode")]
        public async Task<IActionResult> ResendVerificationCode([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { Error = NotRegistered });
            }

            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            _memoryCacheService.Add(user.Email, token, TimeSpan.FromMinutes(1));
            await _emailSender.SendVrfCode(user.Email, token);

            return Ok(new { Message = NewVrfCode });
        }

        [HttpPost("sendRecoverPasswordEmail")]
        public async Task<IActionResult> SendRecoverPasswordEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { Error = NotRegistered });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _memoryCacheService.Add(token, email, TimeSpan.FromMinutes(10));
            await _emailSender.SendPasswordRecoverLink(email, token);

            return Ok(new {Message = "Recover password link was sended to your email."});
        }

        [HttpPost("recoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_memoryCacheService.isExist(request.Token))
            {
                return BadRequest(new {Error = "Invalid token!"});
            }

            var user = await _userManager.FindByEmailAsync(_memoryCacheService.GetValue(request.Token).ToString());
            await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return Ok();
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
