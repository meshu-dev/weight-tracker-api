using Microsoft.AspNetCore.Mvc;

namespace WeightTracker.Api.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        [HttpGet(Name = "GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp" });
        }
    }
}
