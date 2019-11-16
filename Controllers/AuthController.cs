using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;
using WeightTracker.Api.Services;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// Used to login user accounts
    /// </summary>
    [ApiController]
    [Route("auth")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        /// <summary>
        /// Contructor used to create auth controller
        /// </summary>
        public AuthController(
            Repository<UserModel> userRepository,
            JwtService jwtService
        ) {
            _userRepository = (UserRepository) userRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Login as a user
        /// </summary>
        /// <param name="authModel">A model representing the e-mail and password fields</param>
        /// <returns>An ActionResult containing the authorised token</returns>
        /// <response code="200">Returns the id of the created object</response>
        [HttpPost()]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(AuthModel authModel)
        {
            var existingUser = await _userRepository.ReadByEmailAsync(authModel.Email);

            if (existingUser != null)
            {
                if (Crypto.VerifyHashedPassword(existingUser.Password, authModel.Password) == true)
                {
                    var token = _jwtService.CreateToken(authModel.Email);
                    return Ok(new { Token = token });
                }
            }
            return Unauthorized();
        }
    }
}