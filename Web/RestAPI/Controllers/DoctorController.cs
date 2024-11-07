using Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.RequestDtos.Doctor;
using static Common.Errors.Doctor;

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
            var doctors = await _doctorService.GetDoctors(request.Index,
                request.Sorting.ToString(),
                request.Filter,
                request.Search.Trim().ToLower());

            return Ok(doctors);
        }

        [HttpPost("getReviews")]
        public async Task<IActionResult> GetReviews([FromBody] GetReviewsRequest request)
        {
            var reviews = await _doctorService.GetDoctorReviews(request.Index, request.DoctorId);

            return Ok(reviews);
        }

        [HttpPost("addReview")]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            if (!await _doctorService.IsDoctorExist(request.DoctorId))
            {
                return BadRequest(new { Error = InvalidDoctorId });
            }
            
            await _doctorService.AddReview(request.DoctorId, request.Rating, request.Comment, User.Identity.Name);

            return Ok();
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _doctorService.GetSpecialties();

            return Ok(specialties);
        }

        [HttpPost("getAvailableMeetTimes")]
        [Authorize]
        public async Task<IActionResult> GetAvailableMeetTimes([FromBody] GetAvailableMeetTimesRequest request)
        {
            if (!await _doctorService.IsDoctorExist(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidDoctorId });
            }

            if (await _doctorService.IsDayOff(request.DoctorId, request.Date))
            {
                return BadRequest(new { ServerError = InvalidDate });
            }

            var times = await _doctorService.GetAvailableMeetings(request.DoctorId, request.Date);

            return Ok(times);
        }

        [HttpPost("getDaysInMonth")]
        public async Task<IActionResult> GetDaysInMonth([FromBody] GetDaysInMonthRequest request)
        {
            if (!await _doctorService.IsDoctorExist(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidDoctorId });
            }

            var daysInMonth = await _doctorService.GetDaysInMonth(request.DoctorId, request.Month, request.Year);

            return Ok(daysInMonth);
        }

        //public async Task<IActionResult> AddMeeting([FromBody] AddMeetingRequest request)
        //{
        //}
    }
}
