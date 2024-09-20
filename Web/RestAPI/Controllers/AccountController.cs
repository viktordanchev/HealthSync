using RestAPI.DTOs.Account;
using RestAPI.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using static Common.Errors.Account;
using static Common.Messages.Account;

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
        private IVerificationCodeService _vrfCodeService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            ITokenService tokenService,
            IEmailSender emailSender,
            IVerificationCodeService vrfCodeService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _vrfCodeService = vrfCodeService;
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

            var vrfCode = _vrfCodeService.GenerateCode(user.Email);

            var subject = "Confirm your registration!";
            var message = $"<h2>Your verification code: <strong>{vrfCode}</strong>.</h2>";
            await _emailSender.SendEmailAsync(user.Email, subject, message);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin request)
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

        [HttpPost("confirmRegistration")]
        public async Task<IActionResult> ConfirmRegistration([FromBody] ConfirmRegistrationRequest request)
        {
            if (_vrfCodeService.ValidateCode(request.Email, request.VrfCode))
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

            var vrfCode = _vrfCodeService.GenerateCode(email);

            var subject = "Confirm your registration!";
            var message = $"<h2>Your verification code: <strong>{vrfCode}</strong></h2>";
            await _emailSender.SendEmailAsync(user.Email, subject, message);

            return Ok(new { Message = NewVrfCode });
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
