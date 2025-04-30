using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Constants;
using Core.DTOs.RequestDtos.Account;
using Core.Interfaces.ExternalServices;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Common.Errors.Account;
using static Common.Messages.Account;

namespace HealthSync.Server.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private IJwtTokenService _jwtTokenService;
        private IEmailSenderService _emailSender;
        private IMemoryCacheService _memoryCacheService;
        private IBlobStorageServiceService _blobStorageServiceService;
        private IChatService _chatService;
        private readonly IConfiguration _configs;

        public AccountController(
            IUserService userService,
            IJwtTokenService jwtTokenService,
            IEmailSenderService emailSender,
            IMemoryCacheService memoryCacheService,
            IBlobStorageServiceService blobStorageServiceService,
            IChatService chatService,
            IConfiguration configs)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _emailSender = emailSender;
            _memoryCacheService = memoryCacheService;
            _blobStorageServiceService = blobStorageServiceService;
            _chatService = chatService;
            _configs = configs;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!_memoryCacheService.isExist(request.VrfCode.ToLower()))
            {
                return BadRequest(new { Error = InvalidVrfCode });
            }

            var isAdded = await _userService.AddUserAsync(request);

            if (!isAdded)
            {
                return BadRequest(new { Error = UsedEmail });
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!await _userService.IsUserLoginDataValidAsync(request))
            {
                return BadRequest(new { Error = InvalidLoginData });
            }

            var userClaims = await _userService.GetUserClaimsAsync(request.Email);
            var accessToken = _jwtTokenService.GenerateAccessToken(userClaims);

            if (request.RememberMe)
            {
                var refreshToken = _jwtTokenService.GenerateRefreshToken();

                Response.Cookies.Append("refreshToken", refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddMonths(int.Parse(_configs["Cookies:RefreshJWTTokenMonths"])),
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
            if(await _userService.IsUserExistAsync(email))
            {
                return BadRequest(new { Error = UsedEmail });
            }

            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            var isSended = await _emailSender.SendVrfCode(email, token);

            if (isSended)
            {
                _memoryCacheService.Add(token.ToLower(), email, TimeSpan.FromMinutes(1));
            }

            return Ok(new { Message = NewVrfCode });
        }

        [HttpPost("sendRecoverPassEmail")]
        public async Task<IActionResult> SendRecoverPasswordEmail([FromBody] string email)
        {
            var token = await _userService.GeneratePasswordResetTokenAsync(email);
            var isSended = await _emailSender.SendPasswordRecoverLink(email, token);

            if (isSended)
            {
                _memoryCacheService.Add(token, email, TimeSpan.FromMinutes(10));
            }

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
            await _userService.ResetPasswordAsync(request, userEmail);

            return NoContent();
        }

        [HttpGet("getUserData")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var userData = await _userService.GetUserDataAsync(User.FindFirstValue(ClaimTypes.Email)!);

            return Ok(userData);
        }

        [HttpPut("updateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (!string.IsNullOrEmpty(request.CurrentPassword) && request.CurrentPassword == request.NewPassword)
            {
                return BadRequest(new { Error = SamePassword });
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            await _userService.UpdateUserDataAsync(request, userEmail);

            var userClaims = await _userService.GetUserClaimsAsync(userEmail);
            var accessToken = _jwtTokenService.GenerateAccessToken(userClaims);

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

                var userEmail = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userClaims = await _userService.GetUserClaimsAsync(userEmail);

                newAccessToken = _jwtTokenService.GenerateAccessToken(userClaims);
            }

            return Ok(new { Token = newAccessToken });
        }

        [HttpPost("getChatHistory")]
        [Authorize]
        public async Task<IActionResult> GetChatHistory([FromBody] GetChatHistoryRequest request)
        {
            var history = await _chatService.GetChatHistory(request);

            return Ok(history);
        }

        [HttpPost("uploadChatImages")]
        [Authorize]
        public async Task<IActionResult> UploadChatImages([FromForm] IEnumerable<IFormFile> images)
        {
            var imageUrls = await _blobStorageServiceService.UploadChatImagesAsync(images, BlobStorageContainers.ChatImages);

            return Ok(imageUrls);
        }
    }
}
