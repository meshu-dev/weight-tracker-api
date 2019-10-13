using Microsoft.AspNetCore.Mvc;
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

            return Ok(new { Token = token, IsValid = isValid });
        }
    }
}
