using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// Used to retrieve, create and update users
    /// </summary>
    [ApiController]
    [Route("users")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UsersController : Controller
    {
        /// <summary>
        /// Manages users in data store
        /// </summary>
        protected readonly UserRepository userRepository;

        /// <summary>
        /// Manages user roles in data store
        /// </summary>
        protected readonly RoleRepository roleRepository;

        /// <summary>
        /// Manages units in data store
        /// </summary>
        protected readonly UnitRepository unitRepository;

        /// <summary>
        /// Contructor used to create user controller
        /// </summary>
        public UsersController(
            Repository<UserModel> userRepository,
            Repository<RoleModel> roleRepository,
            Repository<UnitModel> unitRepository
        ) {
            this.userRepository = (UserRepository) userRepository;
            this.roleRepository = (RoleRepository) roleRepository;
            this.unitRepository = (UnitRepository) unitRepository;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="model">The user to create</param>
        /// <returns>An ActionResult of type User</returns>
        /// <response code="422">Validation error</response>
        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(UserModel model)
        {
            try
            {
                var role = await roleRepository.ReadAsync(model.RoleId);
                if (role == null) return BadRequest("User role not exist with provided Id");

                var unit = await unitRepository.ReadAsync(model.UnitId);
                if (unit == null) return BadRequest("Unit does not exist with provided Id");

                var user = await userRepository.CreateAsync(model, role, unit);
                if (user == null) return BadRequest("User could not be created");

                return this.StatusCode(StatusCodes.Status201Created, user);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user matching the Id</returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await userRepository.ReadAsync(id);
                if (user == null) return NotFound($"User does not exist with Id {id}");

                return Ok(user);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting user");
            }
        }

        /// <summary>
        /// Get a user by their e-mail address
        /// </summary>
        /// <param name="email">The e-mail address of the user</param>
        /// <returns>The user matching the e-mail address</returns>
        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await userRepository.ReadByEmailAsync(email);
                if (user == null) return NotFound($"User does not exist with email {email}");

                return Ok(user);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting user");
            }
        }

        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns>Multiple users</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await userRepository.ReadAllAsync();
                if (users == null) return NotFound($"No users are available");

                return Ok(users);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="model">The user data to update</param>
        /// <returns>An ActionResult of type User</returns>
        /// <response code="422">Validation error</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, UserModel model)
        {
            try
            {
                var role = await roleRepository.ReadAsync(model.RoleId);
                if (role == null) return BadRequest(new { message = "User role not exist with provided Id" });

                var unit = await unitRepository.ReadAsync(model.UnitId);
                if (unit == null) return BadRequest("Unit does not exist with provided Id");

                var user = await userRepository.ReadAsync3(id);
                if (user == null) return NotFound($"User doesn't exist with Id {id}");

                model.Id = id;

                user = await userRepository.UpdateAsync(model, role, unit);
                if (user == null) return BadRequest("User could not be updated");

                return Ok(user);
            }
            catch (Exception e)
            {
                throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id">The id of the user</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userRepository.ReadAsync2(id);
            if (user == null) return NotFound();

            var isDeleted = await userRepository.DeleteAsync(user);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete User");
        }
    }
}
