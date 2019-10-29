using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;
using WeightTracker.Api.Services;

namespace WeightTracker.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(
            Repository<UserModel> userRepository,
            JwtService jwtService
        ) {
            _userRepository = (UserRepository) userRepository;
            _jwtService = jwtService;
        }

        [HttpPost()]
        [Route("login")]
        public IActionResult Login(JObject json)
        {
            string email = json["email"].Value<string>();
            string password = json["password"].Value<string>();

            var existingUser = _userRepository.ReadByEmail(email);

            if (existingUser != null)
            {
                if (Crypto.VerifyHashedPassword(existingUser.Password, password) == true)
                {
                    var token = _jwtService.CreateToken(email);
                    return Ok(new { Token = token });
                }
            }
            return Unauthorized();
        }
    }
}