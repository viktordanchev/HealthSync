using Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAll();
            return Ok(doctors);
        }
    }
}
