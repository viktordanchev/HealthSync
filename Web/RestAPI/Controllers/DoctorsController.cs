using Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using RestAPI.DTOs.Doctors;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("all")]
        public async Task<IActionResult> Index([FromBody] AllDoctorsRequest request)
        {
            var doctors = await _doctorService.GetDoctors(request.Sorting.ToString(), 
                request.Filter, 
                request.Search.ToLower());

            return Ok(doctors);
        }

        [HttpPost("getReviews")]
        public async Task<IActionResult> GetReviews([FromBody] string doctorId)
        {
            var reviews = await _doctorService.GetDoctorReviews(doctorId);

            return Ok(reviews);
        }
    }
}
