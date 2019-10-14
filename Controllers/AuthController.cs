using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private UserRepository _userRepository;
        private JwtHelper _jwtHelper;

        public AuthController(UserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        [HttpPost()]
        [Route("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            var existingUser = _userRepository.ReadByEmail(user.Email);

            if (existingUser != null)
            {
                if (Crypto.VerifyHashedPassword(existingUser.Password, user.Password) == true)
                {
                    var token = _jwtHelper.CreateToken(user.Email);
                    return Ok(new { Token = token });
                }
            }
            return Unauthorized();
        }
    }
}