using Core.DTOs.RequestDtos.Account;
using Core.DTOs.ResponseDtos.Account;
using Core.Interfaces.ExternalServices;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Common.Errors.Account;
using static Common.Messages.Account;

namespace HealthSync.Server.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IJwtTokenService _jwtTokenService;
        private IEmailSenderService _emailSender;
        private IMemoryCacheService _memoryCacheService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAccountService accountService,
            IJwtTokenService jwtTokenService,
            IEmailSenderService emailSender,
            IMemoryCacheService memoryCacheService,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _jwtTokenService = jwtTokenService;
            _emailSender = emailSender;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!await _accountService.IsUserExistByEmailAsync(request.Email))
            {
                return BadRequest(new { Error = UsedEmail });
            }

            if (!_memoryCacheService.isExist(request.VrfCode.ToLower()))
            {
                return BadRequest(new { Error = InvalidVrfCode });
            }

            await _accountService.AddUserAsync(request);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!await _accountService.IsUserLoggedInAsync(request))
            {
                return BadRequest(new { Error = InvalidLoginData });
            }

            var userData = await _accountService.GetUserDataAsync(request.Email);
            var accessToken = await _jwtTokenService.GenerateAccessTokenAsync(userData);

            if (request.RememberMe)
            {
                var refreshToken = _jwtTokenService.GenerateRefreshToken(userData.Id);

                Response.Cookies.Append("refreshToken", refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddMinutes(5),
                });
            }

            return Ok(new { Token = accessToken });
        }

        [HttpGet("logout")]
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

        [HttpPost("sendVrfCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            if (!await _accountService.IsUserExistByEmailAsync(email))
            {
                return BadRequest(new { Error = UsedEmail });
            }

            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            _memoryCacheService.Add(token.ToLower(), email, TimeSpan.FromMinutes(1));
            await _emailSender.SendVrfCode(email, token);

            return Ok(new { Message = NewVrfCode });
        }

        [HttpPost("sendRecoverPassEmail")]
        public async Task<IActionResult> SendRecoverPasswordEmail([FromBody] string email)
        {
            if (!await _accountService.IsUserExistByEmailAsync(email))
            {
                return BadRequest(new { Error = NotRegistered });
            }

            var token = await _accountService.GeneratePasswordResetTokenAsync(email);
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

            var userEmail = _memoryCacheService.GetValue(request.Token);
            await _accountService.ResetPasswordAsync(request, userEmail);

            return NoContent();
        }

        [HttpGet("getUserData")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userData = await _accountService.GetUserDataAsync(userId);

            return Ok(userData);
        }

        [HttpPut("updateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (request.CurrentPassword != string.Empty)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                if (!isPasswordValid)
                {
                    return BadRequest(new { Error = InvalidCurrentPassword });
                }

                if (request.CurrentPassword == request.NewPassword)
                {
                    return BadRequest(new { Error = SamePassword });
                }
            }

            await _accountService.UpdateUserDataAsync(request, userId);

            var userData = await _accountService.GetUserDataAsync(request.);
            var accessToken = await _jwtTokenService.GenerateAccessTokenAsync(userData);

            return Ok(
                new
                {
                    Message = UserDataUpdated,
                    Token = accessToken
                });
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
                var user = await _userManager.FindByIdAsync(userId);
                var userName = $"{user.FirstName} {user.LastName}";
                var userRole = await _userManager.GetRolesAsync(user);

                newAccessToken = await _jwtTokenService.GenerateAccessTokenAsync(user.Id, userName, userRole);
            }

            return Ok(new { Token = newAccessToken });
        }
    }
}
