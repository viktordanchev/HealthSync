using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("hospitals")]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalsService _hospitalsService;

        public HospitalsController(IHospitalsService hospitalsService)
        {
            _hospitalsService = hospitalsService;
        }

        [HttpGet("getHospitals")]
        public async Task<IActionResult> GetHospitals()
        {
            var hospitals = await _hospitalsService.GetHospitalsAsync();

            return Ok(hospitals);
        }
    }
}
