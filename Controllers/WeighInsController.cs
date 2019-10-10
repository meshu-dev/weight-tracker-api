using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeightTracker.Api.Controllers
{
    [Route("weights")]
    public class WeighInsController : Controller
    {
        private readonly IConfiguration _config;

        public WeighInsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet(Name = "GetWeighIns")]
        public IActionResult GetWeighIns()
        {
            var a = _config.GetConnectionString("SqlServer");
            return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp", Config = a });
        }
    }
}
