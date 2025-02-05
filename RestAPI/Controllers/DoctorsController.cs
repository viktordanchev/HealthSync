using Core.Interfaces.Service;
using Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Dtos.RequestDtos.Doctors;
using System.Security.Claims;
using static Common.Errors;
using static Common.Messages.Doctors;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IDoctorsService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IHospitalsService _hospitalsService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ISpecialtiesService _specialtiesService;

        public DoctorsController(
            IDoctorsService doctorService,
            IDoctorScheduleService doctorScheduleService,
            IHospitalsService hospitalsService,
            UserManager<ApplicationUser> userManager,
            IJwtTokenService jwtTokenService,
            ISpecialtiesService specialtiesService)
        {
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
            _hospitalsService = hospitalsService;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _specialtiesService = specialtiesService;
        }

        [HttpPost("getDoctors")]
        public async Task<IActionResult> GetDoctors([FromBody] GetDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctorsAsync(request,
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(doctors);
        }

        [HttpPost("getDoctorDetails")]
        public async Task<IActionResult> GetDoctorDetails([FromBody] int doctorId)
        {
            if (!await _doctorService.IsDoctorExistAsync(doctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var doctor = await _doctorService.GetDoctorDetailsAsync(doctorId);

            return Ok(doctor);
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _specialtiesService.GetSpecialtiesAsync();

            return Ok(specialties);
        }

        [HttpPost("getAvailableMeetingHours")]
        public async Task<IActionResult> GetAvailableMeetingHours([FromBody] GetAvailableMeetingHours request)
        {
            var date = DateTime.Parse(request.Date);

            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId) ||
                await _doctorScheduleService.IsDayOffAsync(request.DoctorId, date))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var times = await _doctorScheduleService.GetAvailableMeetingsAsync(request.DoctorId, date);

            return Ok(times);
        }

        [HttpPost("getMonthSchedule")]
        public async Task<IActionResult> GetMonthSchedule([FromBody] GetMonthScheduleRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var monthSchedule = await _doctorScheduleService.GetMonthScheduleAsync(request.DoctorId, request.Month, request.Year);

            return Ok(monthSchedule);
        }

        [HttpPost("becomeDoctor")]
        [Authorize]
        public async Task<IActionResult> BecomeDoctor([FromForm] BecomeDoctorRequest request)
        {
            if (await _doctorService.IsUserDoctorAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)) ||
                !await _hospitalsService.IsHospitalExistAsync(request.HospitalId) ||
                !await _specialtiesService.IsSpecialtyExistAsync(request.SpecialtyId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _doctorService.AddDoctorAsync(request, user.Id);
            await _userManager.AddToRoleAsync(user, "Doctor");

            var accessToken = await _jwtTokenService.GenerateAccessTokenAsync(user.Id);

            return Ok(
                new
                {
                    Message = RegisteredDoctor,
                    Token = accessToken
                });
        }

        [HttpGet("getDoctorInfo")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var doctorInfo = await _doctorService.GetDoctorPersonalInfoAsync(userId);

            return Ok(doctorInfo);
        }
    }
}
