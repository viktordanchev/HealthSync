using Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.RequestDtos.Doctors;
using static Common.Errors.Doctors;

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
                request.Search.ToLower());

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
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(ms => ms.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                return BadRequest(errors);
            }

            if (!await _doctorService.IsDoctorExist(request.DoctorId))
            {
                return BadRequest(new { Error = InvalidDoctorId });
            }

            await _doctorService.AddReview(request.DoctorId, request.Rating, request.Reviewer);

            return Ok();
        }

        [HttpGet("getSpecialties")]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _doctorService.GetSpecialties();

            return Ok(specialties);
        }
    }
}
