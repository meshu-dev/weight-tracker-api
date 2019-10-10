using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;

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

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp" });
        }
    }
}
