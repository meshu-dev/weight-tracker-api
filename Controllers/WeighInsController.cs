using Microsoft.AspNetCore.Mvc;

namespace WeightTracker.Api.Controllers
{
    [Route("weights")]
    public class WeighInsController : Controller
    {
        [HttpGet(Name = "GetWeighIns")]
        public IActionResult GetWeighIns()
        {
            return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp" });
        }
    }
}
