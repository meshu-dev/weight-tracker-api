using Microsoft.AspNetCore.Mvc;
using System;
using WeightTracker.Api.Helpers;

namespace WeightTracker.Api.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private JwtHelper _jwtHelper;

        public HomeController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

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
            var token = _jwtHelper.CreateToken("Joker2");
            var isValid = _jwtHelper.VerifyToken(token);

            return Ok(new { Token = token, IsValid = isValid, TokenExpires = (DateTime.Now.AddMinutes(1)).ToString() });
        }

        [HttpGet]
        [Route("test2")]
        public IActionResult Test2()
        {
            //var token = _jwtHelper.CreateToken("Joker2");

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnaXZlbl9uYW1lIjoiSm9rZXIyIiwiZXhwIjoxNTcyMzEzMTY3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYzOTM5LyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjM5MzkvIn0._ioIrkwCK2pcd8YQ_UzPYLTlLsKSXHc2PwF5p3mbpC4";
            var isValid = _jwtHelper.VerifyToken(token);

            return Ok(new { Token = token, IsValid = isValid });
        }
    }
}
