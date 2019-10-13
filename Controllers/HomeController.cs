using Microsoft.AspNetCore.Mvc;

namespace WeightTracker.Api.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok(new { Status = "Ok" });
        }
    }
}
