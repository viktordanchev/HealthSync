using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.DoctorSchedule;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Common.Errors;
using static Common.Messages.Doctors;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDoctorsService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IJwtTokenService _jwtTokenService;

        public DoctorsController(
            IUserService userService,
            IDoctorsService doctorService,
            IDoctorScheduleService doctorScheduleService,
            IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("getDoctors")]
        public async Task<IActionResult> GetDoctors([FromBody] GetDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctorsAsync(request,
                User.FindFirstValue(ClaimTypes.Email));

            return Ok(doctors);
        }

        [HttpPost("getDetails")]
        public async Task<IActionResult> GetDetails([FromBody] int doctorId)
        {
            try
            {
                var doctorDetails = await _doctorService.GetDoctorDetailsAsync(doctorId);
                return Ok(doctorDetails);
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _doctorService.GetSpecialtiesAsync();

            return Ok(specialties);
        }

        [HttpPost("getAvailableMeetingHours")]
        public async Task<IActionResult> GetAvailableMeetingHours([FromBody] GetAvailableMeetingHours request)
        {
            var dateParsed = DateTime.Parse(request.Date);

            try
            {
                if (await _doctorScheduleService.IsDateUnavailableAsync(request.DoctorId, dateParsed) || DateTime.Now.Date > dateParsed)
                {
                    return BadRequest(new { ServerError = InvalidRequest });
                }

                var times = await _doctorScheduleService.GetAvailableMeetingsAsync(request.DoctorId, dateParsed);
                return Ok(times);
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpPost("getMonthSchedule")]
        public async Task<IActionResult> GetMonthSchedule([FromBody] GetMonthScheduleRequest request)
        {
            try
            {
                var monthSchedule = await _doctorScheduleService.GetMonthScheduleAsync(request);
                return Ok(monthSchedule);
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpPost("becomeDoctor")]
        [Authorize]
        public async Task<IActionResult> BecomeDoctor([FromForm] ProfileInfoRequest request)
        {
            try
            {
                await _doctorService.AddDoctorAsync(request, User.FindFirstValue(ClaimTypes.NameIdentifier));

                var userClaims = await _userService.GetUserClaimsAsync(User.FindFirstValue(ClaimTypes.Email));
                var accessToken = _jwtTokenService.GenerateAccessToken(userClaims);

                return Ok(
                    new
                    {
                        Message = RegisteredDoctor,
                        Token = accessToken
                    });
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpGet("getProfileInfo")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetProfileInfo()
        {
            var doctorInfo = await _doctorService.GetDoctorPersonalInfoAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(doctorInfo);
        }

        [HttpGet("getDaysOff")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDaysOff()
        {
            var daysOff = await _doctorScheduleService.GetAllDaysOffAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(daysOff);
        }

        [HttpPost("updateWeeklySchedule")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateWeeklySchedule([FromBody] IEnumerable<UpdateWeeklyScheduleRequest> weeklySchedule)
        {
            await _doctorScheduleService.UpdateWeeklySchedule(weeklySchedule);

            return Ok(new { Message = UpdatedWeeklySchedule });
        }

        [HttpPost("updateDaysOff")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDaysOff([FromBody] IEnumerable<DayOffResponse> daysOff)
        {
            await _doctorScheduleService.UpdateDaysOffAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!, daysOff);

            return Ok(new { Message = UpdatedDaysOff });
        }

        [HttpPost("updateProfileInfo")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateProfileInfo([FromBody] ProfileInfoRequest data)
        {
            await _doctorService.UpdateProfileInfo(data, User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok();
        }

        [HttpGet("getTopDoctors")]
        public async Task<IActionResult> GetTopDoctors()
        {
            var doctors = await _doctorService.GetTopDoctorsAsync();

            return Ok(doctors);
        }
    }
}
