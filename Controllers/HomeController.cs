using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// The main index controller for API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the current status of the API
        /// </summary>
        /// <returns>The status of the API</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok(new { Status = "Ok" });
        }

        /// <summary>
        /// Used to test out functions of API
        /// </summary>
        /// <returns>Data related to the test function used</returns>
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var userId = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var role = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;

            return Ok(new { Status = "Test Ok", userId = userId, role = role });
        }
    }
}
