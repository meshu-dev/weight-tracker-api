using Microsoft.AspNetCore.Mvc;
using System;
using WeightTracker.Api.Helpers;

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

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok(new { Status = "Test Ok" });
        }
    }
}
