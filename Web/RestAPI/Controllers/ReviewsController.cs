using Core.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Dtos.RequestDtos.Doctor;
using static Common.Errors;
using static Common.Messages.Reviews;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IDoctorsService _doctorService;
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IDoctorsService doctorService, IReviewsService reviewsService)
        {
            _doctorService = doctorService;
            _reviewsService = reviewsService;
        }

        [HttpPost("getDoctorReviews")]
        public async Task<IActionResult> GetDoctorReviews([FromBody] GetReviewsRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            var reviews = await _reviewsService.GetDoctorReviewsAsync(request.Index, request.DoctorId);

            return Ok(reviews);
        }

        [HttpPost("addDoctorReview")]
        [Authorize]
        public async Task<IActionResult> AddDoctorReview([FromBody] AddReviewRequest request)
        {
            if (!await _doctorService.IsDoctorExistAsync(request.DoctorId))
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }

            await _reviewsService.AddDoctorReviewAsync(request.DoctorId, request.Rating, request.Comment, User.Identity.Name);

            return Ok(new { Message = AddedReview });
        }
    }
}
