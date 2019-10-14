using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private Repository<UserModel> _userRepository;
        private JwtHelper _jwtHelper;

        public AuthController(Repository<UserModel> userRepository, JwtHelper jwtHelper)
        {
            _userRepository = (UserRepository) userRepository;
            _jwtHelper = jwtHelper;
        }

        [HttpPost()]
        [Route("login")]
        public IActionResult Login(JObject json)
        {
            string email = json["Email"].Value<string>();
            string password = json["Password"].Value<string>();

            var existingUser = ((UserRepository) _userRepository).ReadByEmail(email);

            if (existingUser != null)
            {
                if (Crypto.VerifyHashedPassword(existingUser.Password, password) == true)
                {
                    var token = _jwtHelper.CreateToken(email);
                    return Ok(new { Token = token });
                }
            }
            return Unauthorized();
        }
    }
}