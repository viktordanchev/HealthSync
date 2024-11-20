using Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.RequestDtos.Doctor;
using System.Security.Claims;
using static Common.Errors;
using static Common.Messages.Doctor;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("all")]
        public async Task<IActionResult> Index([FromBody] AllDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctorsAsync(request.Index,
                request.Sorting.ToString(),
                request.Filter,
                request.Search.Trim().ToLower());

            return Ok(doctors);
        }

        [HttpPost("getDoctorDetails")]
        public async Task<IActionResult> GetDoctorDetails([FromBody] int doctorId)
        {
            if (!await _doctorService.IsDoctorExistAsync(doctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            return Ok(doctor);
        }

        [HttpPost("getReviews")]
        public async Task<IActionResult> GetReviews([FromBody] GetReviewsRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var reviews = await _doctorService.GetDoctorReviewsAsync(request.Index, request.DoctorId);

            return Ok(reviews);
        }

        [HttpPost("addReview")]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            await _doctorService.AddReviewAsync(request.DoctorId, request.Rating, request.Comment, User.Identity.Name);

            return Ok(new { Message = AddedReview });
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _doctorService.GetSpecialtiesAsync();

            return Ok(specialties);
        }

        [HttpPost("getAvailableMeetTimes")]
        [Authorize]
        public async Task<IActionResult> GetAvailableMeetTimes([FromBody] GetAvailableMeetTimesRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            if (await _doctorService.IsDayOffAsync(request.DoctorId, request.Date))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var times = await _doctorService.GetAvailableMeetingsAsync(request.DoctorId, request.Date);

            return Ok(times);
        }

        [HttpPost("getDaysInMonth")]
        public async Task<IActionResult> GetDaysInMonth([FromBody] GetDaysInMonthRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var daysInMonth = await _doctorService.GetDaysInMonthAsync(request.DoctorId, request.Month, request.Year);

            return Ok(daysInMonth);
        }

        [HttpPost("addMeeting")]
        [Authorize]
        public async Task<IActionResult> AddMeeting([FromBody] AddMeetingRequest request)
        {
            var localDateTime = request.Date.ToLocalTime();

            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId) || 
                !await _doctorService.IsDateValidAsync(request.DoctorId, localDateTime))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            await _doctorService.AddMeetingAsync(request.DoctorId, localDateTime, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(new { Message = AddedMeeting });
        }
    }
}
