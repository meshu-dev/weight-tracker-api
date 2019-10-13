using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeightTracker.Api.Controllers
{
    [Route("weights")]
    [ApiController]
    public class WeighInsController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp" });
        }
    }
}
