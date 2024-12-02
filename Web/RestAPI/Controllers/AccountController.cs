using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.RequestDtos.Account;
using RestAPI.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Common.Errors;
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
        private IJWTTokenService _tokenService;
        private IEmailSender _emailSender;
        private IMemoryCacheService _memoryCacheService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJWTTokenService tokenService,
            IEmailSender emailSender,
            IMemoryCacheService memoryCacheService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return Ok();
                }

                return BadRequest(new { Error = UsedEmail });
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
                return BadRequest(new { ServerError = InvalidRequest });
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized(new { Error = InvalidLoginData });
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest(new { NotVerified = true });
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest(new { Error = InvalidLoginData });
            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user.Id);

            if (request.RememberMe)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
                var refreshTokenExpireTime = _tokenService.GetTokenExpireTime(refreshToken);
                _tokenService.AppendRefreshTokenToCookie(HttpContext, refreshToken, refreshTokenExpireTime);
            }

            return Ok(new { Token = accessToken });
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("refreshToken"))
            {
                Response.Cookies.Append("refreshToken", "", new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                    HttpOnly = true,
                    Secure = true,
                });
            }

            return NoContent();
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

        [HttpPost("sendVrfCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized(new { Error = NotRegistered });
            }
            else if (user.EmailConfirmed)
            {
                return BadRequest(new { Error = AlredyVerified });
            }

            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            _memoryCacheService.Add(user.Email, token, TimeSpan.FromMinutes(1));
            await _emailSender.SendVrfCode(user.Email, token);

            return Ok(new { Message = NewVrfCode });
        }

        [HttpPost("sendRecoverPassEmail")]
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

            return Ok(new { Message = SendedPassRecoverLink });
        }

        [HttpPost("recoverPass")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            if (!_memoryCacheService.isExist(request.Token))
            {
                return BadRequest(new { Error = InvalidToken });
            }

            var user = await _userManager.FindByEmailAsync(_memoryCacheService.GetValue(request.Token).ToString());
            await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return Ok();
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var newAccessToken = string.Empty;

            if (refreshToken != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(refreshToken);
                var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                newAccessToken = await _tokenService.GenerateAccessTokenAsync(userId);
            }

            return Ok(new { Token = newAccessToken });
        }
    }
}
