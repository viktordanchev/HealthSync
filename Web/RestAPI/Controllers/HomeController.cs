using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [HttpGet("da")]
        public IActionResult Da()
        {
            return Ok(new { data = "text nqkuv" });
        }
    }
}
