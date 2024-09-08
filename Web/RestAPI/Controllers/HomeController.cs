using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }


        [HttpGet("da")]
        public IActionResult Da()
        {
            return Ok();
        }
    }
}
