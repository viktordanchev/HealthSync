using Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Dtos.RequestDtos.Doctor;
using System.Security.Claims;
using static Common.Errors;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsService _doctorService;
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorsController(IDoctorsService doctorService, IDoctorScheduleService doctorScheduleService)
        {
            _doctorService = doctorService;
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost("getDoctors")]
        public async Task<IActionResult> GetDoctors([FromBody] AllDoctorsRequest request)
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
        public async Task<IActionResult> GetAvailableMeetingHours([FromBody] GetAvailableMeetTimesRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId) ||
                await _doctorScheduleService.IsDayOffAsync(request.DoctorId, request.Date))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var times = await _doctorScheduleService.GetAvailableMeetingsAsync(request.DoctorId, request.Date);

            return Ok(times);
        }

        [HttpPost("getMonthShedule")]
        public async Task<IActionResult> GetMonthSchedule([FromBody] GetMonthScheduleRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var daysInMonth = await _doctorScheduleService.GetMonthScheduleAsync(request.DoctorId, request.Month, request.Year);

            return Ok(daysInMonth);
        }
    }
}
