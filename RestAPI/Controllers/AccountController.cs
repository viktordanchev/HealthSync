﻿using Core.Interfaces.ExternalServices;
using Core.Interfaces.Service;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Dtos.RequestDtos.Account;
using RestAPI.Dtos.ResponseDtos.Account;
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
        private IJwtTokenService _jwtTokenService;
        private IEmailSenderService _emailSender;
        private IMemoryCacheService _memoryCacheService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwtTokenService,
            IEmailSenderService emailSender,
            IMemoryCacheService memoryCacheService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
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
                return BadRequest(new { Error = UsedEmail });
            }

            if (!_memoryCacheService.isExist(request.VrfCode.ToLower()))
            {
                return BadRequest(new { Error = InvalidVrfCode });
            }

            user = new ApplicationUser()
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized(new { Error = InvalidLoginData });
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest(new { Error = InvalidLoginData });
            }

            var accessToken = await _jwtTokenService.GenerateAccessTokenAsync(user.Id);

            if (request.RememberMe)
            {
                var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

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
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
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

            return NoContent();
        }

        [HttpGet("getUserData")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var userData = new UserDataResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userData);
        }

        [HttpPut("updateUser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

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

                await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            await _userManager.UpdateAsync(user);

            var accessToken = await _jwtTokenService.GenerateAccessTokenAsync(user.Id);

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

                newAccessToken = await _jwtTokenService.GenerateAccessTokenAsync(userId);
            }

            return Ok(new { Token = newAccessToken });
        }
    }
}
