using Core.Services.Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Dtos.RequestDtos.Doctors;
using RestAPI.Services.Contracts;
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
        private readonly IGoogleCloudStorageService _GCSService;
        private readonly IJWTTokenService _jwtService;

        public DoctorsController(
            IDoctorsService doctorService,
            IDoctorScheduleService doctorScheduleService,
            IHospitalsService hospitalsService,
            IGoogleCloudStorageService googleCloudStorage,
            UserManager<ApplicationUser> userManager,
            IJWTTokenService jwtService)
        {
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
            _hospitalsService = hospitalsService;
            _GCSService = googleCloudStorage;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("getDoctors")]
        public async Task<IActionResult> GetDoctors([FromBody] GetDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctorsAsync(request.Index,
                request.Sorting.ToString(),
                request.Filter,
                request.Search.Trim().ToLower(),
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
            var specialties = await _doctorService.GetSpecialtiesAsync();

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
                !await _doctorService.IsSpecialtyExistAsync(request.SpecialtyId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            string? imgUrl = null;

            if (request.ProfilePhoto != null)
            {
                imgUrl = await _GCSService.UploadProfileImageAsync(request.ProfilePhoto);
            }

            await _doctorService.AddDoctorAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                request.HospitalId,
                request.SpecialtyId,
                request.ContactEmail,
                request.ContactPhoneNumber,
                imgUrl);

            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _userManager.AddToRoleAsync(user, "Doctor");

            var accessToken = await _jwtService.GenerateAccessTokenAsync(user.Id);

            return Ok(
                new
                {
                    Message = RegisteredDoctor,
                    Token = accessToken
                });
        }

        [HttpPost("getDoctorInfo")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok();
        }
    }
}
