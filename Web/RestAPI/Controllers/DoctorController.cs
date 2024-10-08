using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> All()
        {

        }
    }
}
