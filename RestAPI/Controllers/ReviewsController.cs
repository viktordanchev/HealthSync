using Core.DTOs.RequestDtos.Reviews;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Common.Errors;
using static Common.Messages.Reviews;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(
            IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpPost("getDoctorReviews")]
        public async Task<IActionResult> GetDoctorReviews([FromBody] GetReviewsRequest request)
        {
            try
            {
                var reviews = await _reviewsService.GetDoctorReviewsAsync(request);

                return Ok(reviews);
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }

        [HttpPost("addDoctorReview")]
        [Authorize]
        public async Task<IActionResult> AddDoctorReview([FromBody] AddReviewRequest request)
        {
            try
            {
                await _reviewsService.AddDoctorReviewAsync(request, User.Identity.Name);

                return Ok(new { Message = AddedReview });
            }
            catch
            {
                return BadRequest(new { ServerError = InvalidRequest });
            }
        }
    }
}
