using Microsoft.AspNetCore.Mvc;

namespace WeightTracker.Api.Controllers
{
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
